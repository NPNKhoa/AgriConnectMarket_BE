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
using System.Text;

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

        public async Task<Result<CreatePaymentResponseDto>> CreatePaymentUrlAsync(IEnumerable<Guid> orderIds, string clientIp = "127.0.0.1", CancellationToken ct = default)
        {
            if (!orderIds.Any())
            {
                return Result<CreatePaymentResponseDto>.Fail(MessageConstant.ORDER_ID_REQUIRED);
            }

            long amountInCents = 0;
            var txnRef = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var orderInfo = new StringBuilder();

            var tx = Transaction.Create(txnRef, amountInCents / 100, _dateTimeProvider.UtcNow);
            await _uow.TransactionRepository.AddAsync(tx, ct);
            await _uow.SaveChangesAsync(ct);

            foreach (var id in orderIds)
            {
                var order = await _uow.OrderRepository.GetByIdAsync(id, ct);

                if (order is null)
                {
                    return Result<CreatePaymentResponseDto>.Fail($"{MessageConstant.ORDER_NOT_FOUND} with ID: {id}");
                }

                orderInfo.Append($"{order.OrderCode.ToString()}");
                amountInCents = (long)(order.TotalPrice * 100);

                order.TransactionId = tx.Id;

                await _uow.OrderRepository.UpdateAsync(order, ct);
                await _uow.SaveChangesAsync(ct);
            }

            // Build parameters
            var vnpParams = new Dictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", _settings.TmnCode },
                { "vnp_Amount", amountInCents.ToString() },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", txnRef },
                { "vnp_OrderInfo", orderInfo.ToString() },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", _settings.Locale ?? "vn" },
                { "vnp_ReturnUrl", _settings.ReturnUrl },
                { "vnp_IpAddr", clientIp ?? "127.0.0.1" },
                { "vnp_CreateDate", _dateTimeProvider.UtcNow.ToString("yyyyMMddHHmmss") }
            };

            // Build URL with secure hash
            var vnpUrl = VnPayHelper.BuildVnPayUrl(_settings.Url, _settings.HashSecret, vnpParams);

            var responseDto = new CreatePaymentResponseDto { PaymentUrl = vnpUrl };

            return Result<CreatePaymentResponseDto>.Success(responseDto);
        }

        public async Task<Result<VnPayResponseDto>> HandleReturnAsync(IQueryCollection query, CancellationToken ct = default)
        {
            // Client returns here after payment (browser redirect)
            var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            if (!VnPayHelper.ValidateSignature(dict, _settings.HashSecret))
                return Result<VnPayResponseDto>.Fail(MessageConstant.TRANSACTION_FAIL);

            // read vnp_TxnRef, vnp_ResponseCode, vnp_TransactionNo, etc.
            dict.TryGetValue("vnp_TxnRef", out var txnRef);
            dict.TryGetValue("vnp_ResponseCode", out var responseCode); // "00" success
            dict.TryGetValue("vnp_BankCode", out var bankCode);
            dict.TryGetValue("vnp_OrderInfo", out var orderCode);

            if (responseCode is null || bankCode is null || responseCode != "00")
            {
                return Result<VnPayResponseDto>.Fail(MessageConstant.TRANSACTION_FAIL);
            }

            var tx = await _uow.TransactionRepository.GetTransactionByRef(txnRef!, true, ct);

            tx.UpdateTranasctionStatus(responseCode!, bankCode!);
            tx.UpdateBankCode(bankCode);

            await _uow.TransactionRepository.UpdateAsync(tx, ct);

            foreach (var order in tx.Orders)
            {
                order.UpdatePaymentStatus(txAmount: tx.Amount, txUpdatedAt: tx.UpdatedAt ?? _dateTimeProvider.UtcNow);
                await _uow.OrderRepository.UpdateAsync(order, ct);
            }

            await _uow.SaveChangesAsync(ct);

            return Result<VnPayResponseDto>.Success(new VnPayResponseDto(responseCode, orderCode!));
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
