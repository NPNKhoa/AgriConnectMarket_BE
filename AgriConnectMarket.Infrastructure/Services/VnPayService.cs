//using AgriConnectMarket.Application.DTOs.ResponseDtos;
//using AgriConnectMarket.Application.Interfaces;
//using AgriConnectMarket.Infrastructure.Data;
//using AgriConnectMarket.Infrastructure.Repositories;
//using AgriConnectMarket.Infrastructure.Settings;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Options;

//namespace AgriConnectMarket.Infrastructure.Services
//{
//    public class VnPayService
//    {
//        private readonly VnPaySettings _settings;
//        private readonly IUnitOfWork _uow;

//        public VnPayService(IOptions<VnPaySettings> options, UnitOfWork uow)
//        {
//            _settings = options.Value;
//            _uow = uow;
//        }

//        public async Task<CreatePaymentResponseDto> CreatePaymentUrlAsync(CreatePaymentRequestDto req)
//        {
//            var order = await _repo.GetOrderAsync(req.OrderId)
//                ?? throw new InvalidOperationException("Order not found");

//            // VNPay expects integer amount in smallest unit (VND * 100)
//            long amountInCents = (long)(req.Amount * 100);

//            // vnp_TxnRef: unique transaction reference in your system (string)
//            var txnRef = Guid.NewGuid().ToString("N");

//            // Build parameters
//            var vnpParams = new Dictionary<string, string>
//        {
//            { "vnp_Version", "2.1.0" },
//            { "vnp_Command", "pay" },
//            { "vnp_TmnCode", _settings.TmnCode },
//            { "vnp_Amount", amountInCents.ToString() },
//            { "vnp_CurrCode", "VND" },
//            { "vnp_TxnRef", txnRef },
//            { "vnp_OrderInfo", req.OrderDescription },
//            { "vnp_OrderType", "other" },
//            { "vnp_Locale", _settings.Locale ?? "vn" },
//            { "vnp_ReturnUrl", _settings.ReturnUrl },
//            { "vnp_IpAddr", req.ClientIp },
//            { "vnp_CreateDate", DateTime.UtcNow.ToString("yyyyMMddHHmmss") }
//            // you can add vnp_ExpireDate etc.
//        };

//            // Build URL with secure hash
//            var vnpUrl = VnPayHelper.BuildVnPayUrl(_settings.Url, _settings.HashSecret, vnpParams);

//            // Save transaction placeholder (so you can match it on return/ipn)
//            var tx = new PaymentTransaction
//            {
//                OrderId = order.Id,
//                VnPayTxnRef = txnRef,
//                Amount = req.Amount,
//                Status = "Pending",
//                CreatedAt = DateTime.UtcNow
//            };
//            await _repo.SavePaymentTransactionAsync(tx);

//            return new CreatePaymentResponseDto { PaymentUrl = vnpUrl };
//        }

//        public async Task<bool> HandleReturnAsync(IQueryCollection query)
//        {
//            // Client returns here after payment (browser redirect)
//            var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
//            if (!VnPayHelper.ValidateSignature(dict, _settings.HashSecret))
//                return false;

//            // read vnp_TxnRef, vnp_ResponseCode, vnp_TransactionNo, etc.
//            dict.TryGetValue("vnp_TxnRef", out var txnRef);
//            dict.TryGetValue("vnp_ResponseCode", out var responseCode); // "00" success

//            // find tx by txnRef (repo method not shown; add it)
//            // if success, mark order paid and update tx
//            // For demo, we'll assume we can find and mark paid
//            // TODO: implement GetTransactionByTxnRef in repository

//            // Example:
//            // var tx = await _repo.GetByTxnRefAsync(txnRef);
//            // if (tx != null && responseCode == "00") { await _repo.MarkOrderPaidAsync(...); update tx status; ... }

//            return responseCode == "00";
//        }

//        public async Task<bool> HandleIpnAsync(IQueryCollection query)
//        {
//            // IPN is server-to-server; VNPay posts query string params
//            var dict = query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
//            if (!VnPayHelper.ValidateSignature(dict, _settings.HashSecret))
//                return false;

//            dict.TryGetValue("vnp_TxnRef", out var txnRef);
//            dict.TryGetValue("vnp_ResponseCode", out var responseCode);

//            // TODO: lookup transaction by txnRef, verify amount matches, update statuses
//            // If success: mark order paid, update payment transaction record and respond "OK"
//            return responseCode == "00";
//        }
//    }
//}
