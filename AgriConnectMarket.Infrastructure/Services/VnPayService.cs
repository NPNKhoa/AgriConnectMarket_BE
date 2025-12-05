using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Payment;
using AgriConnectMarket.Infrastructure.Settings;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class VnPayService
    {
        private readonly VnPaySettings _settings;
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _dateTimeProvider;

        public VnPayService(IOptions<VnPaySettings> options, IUnitOfWork uow, IDateTimeProvider dateTimeProvider)
        {
            _settings = options.Value;
            _uow = uow;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<CreatePaymentResponseDto>> CreatePaymentUrlAsync(Guid orderId, string clientIp = "127.0.0.1", CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByIdAsync(orderId, ct);

            if (order is null)
            {
                return Result<CreatePaymentResponseDto>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            // VNPay expects integer amount in smallest unit (VND * 100)
            long amountInCents = (long)(order.TotalPrice * 100);

            // vnp_TxnRef: unique transaction reference in your system (string)
            string orderDesc = $"Pay for the order {order.OrderCode}";

            var txnRef = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();


            // Build parameters
            var vnpParams = new Dictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", _settings.TmnCode },
                { "vnp_Amount", amountInCents.ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", txnRef },
                { "vnp_OrderInfo", orderDesc },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", _settings.Locale ?? "vn" },
                { "vnp_ReturnUrl", _settings.ReturnUrl },
                { "vnp_IpAddr", clientIp ?? "127.0.0.1" },
                { "vnp_CreateDate", _dateTimeProvider.UtcNow.ToString("yyyyMMddHHmmss") }
            };

            // Build URL with secure hash
            var vnpUrl = VnPayHelper.BuildVnPayUrl(_settings.Url, _settings.HashSecret, vnpParams);

            // Save transaction placeholder (so you can match it on return/ipn)
            var tx = new Transaction
            {
                OrderId = order.Id,
                TransactionRef = txnRef,
                TransactionNo = order.OrderCode,
                Amount = order.TotalPrice,
                Status = TransactionStatusConst.PENDING,
                BankCode = "",
                CreatedAt = DateTime.UtcNow
            };

            await _uow.TransactionRepository.AddAsync(tx, ct);
            await _uow.SaveChangesAsync(ct);

            var responseDto = new CreatePaymentResponseDto { PaymentUrl = vnpUrl };

            return Result<CreatePaymentResponseDto>.Success(responseDto);
        }

        public async Task<Result<bool>> HandleReturnAsync(IQueryCollection query, CancellationToken ct = default)
        {
            // Client returns here after payment (browser redirect)
            var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            if (!VnPayHelper.ValidateSignature(dict, _settings.HashSecret))
                return Result<bool>.Fail(MessageConstant.TRANSACTION_FAIL);

            // read vnp_TxnRef, vnp_ResponseCode, vnp_TransactionNo, etc.
            dict.TryGetValue("vnp_TxnRef", out var txnRef);
            dict.TryGetValue("vnp_ResponseCode", out var responseCode); // "00" success
            dict.TryGetValue("vnp_BankCode", out var bankCode);

            // find tx by txnRef (repo method not shown; add it)
            // if success, mark order paid and update tx
            // For demo, we'll assume we can find and mark paid
            // TODO: implement GetTransactionByTxnRef in repository

            if (responseCode is null || bankCode is null || responseCode != "00")
            {
                return Result<bool>.Fail(MessageConstant.TRANSACTION_FAIL);
            }

            var tx = await _uow.TransactionRepository.GetTransactionByRef(txnRef!, true, ct);

            tx.UpdateTranasctionStatus(responseCode!, bankCode!);
            tx.Order.UpdatePaymentStatus(txAmount: tx.Amount, txUpdatedAt: tx.UpdatedAt ?? _dateTimeProvider.UtcNow);

            // Example:
            // var tx = await _repo.GetByTxnRefAsync(txnRef);
            // if (tx != null && responseCode == "00") { await _repo.MarkOrderPaidAsync(...); update tx status; ... }

            return Result<bool>.Success(responseCode == "00");
        }

        public async Task<bool> HandleIpnAsync(IQueryCollection query)
        {
            // IPN is server-to-server; VNPay posts query string params
            var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            if (!VnPayHelper.ValidateSignature(dict, _settings.HashSecret))
                return false;

            dict.TryGetValue("vnp_TxnRef", out var txnRef);
            dict.TryGetValue("vnp_ResponseCode", out var responseCode);

            // TODO: lookup transaction by txnRef, verify amount matches, update statuses
            // If success: mark order paid, update payment transaction record and respond "OK"
            return responseCode == "00";
        }
    }
}
