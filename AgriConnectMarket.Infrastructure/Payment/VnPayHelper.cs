using AgriConnectMarket.SharedKernel.Constants;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Payment
{
    public static class VnPayHelper
    {
        private static readonly Dictionary<string, string> _errors = new()
        {
            // ===== Common / Payment =====
            ["00"] = "Transaction successful",
            ["07"] = "Transaction successful but marked as suspicious",
            ["09"] = "Card/Account is not registered for Internet Banking",
            ["10"] = "Authentication failed more than allowed attempts",
            ["11"] = "Payment timeout",
            ["12"] = "Card/Account is locked",
            ["13"] = "Incorrect OTP",
            ["24"] = "Transaction cancelled by user",
            ["51"] = "Insufficient account balance",
            ["65"] = "Daily transaction limit exceeded",
            ["75"] = "Bank system under maintenance",
            ["79"] = "Payment password incorrect too many times",
            ["99"] = "Unknown error",

            // ===== Query / Refund / API =====
            ["02"] = "Invalid merchant (check vnp_TmnCode)",
            ["03"] = "Invalid data format",
            ["04"] = "Full refund is not allowed after partial refund",
            ["13R"] = "Only partial refund is allowed",
            ["91"] = "Transaction not found",
            ["93"] = "Invalid refund amount",
            ["94"] = "Duplicate request within allowed time",
            ["95"] = "Transaction failed at VNPay",
            ["97"] = "Invalid signature",
            ["98"] = "Timeout exception"
        };

        /// <summary>
        /// Get VNPay error description by error code
        /// </summary>
        public static string GetDescription(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return MessageConstant.INVALID_VNPAY_ERR_CODE;

            return _errors.TryGetValue(code, out var description)
                ? description
                : MessageConstant.UNKNOWN_ERROR;
        }

        /// <summary>
        /// Build VNPay URL from base url, secret and parameters
        /// </summary>
        public static string BuildVnPayUrl(string baseUrl, string hashSecret, IDictionary<string, string> input)
        {
            // 1. Sort input by key ascending
            var sorted = input.OrderBy(k => k.Key, StringComparer.Ordinal);

            // 2. Build query string (URL-encoded values). For hash, we create the hashData by concatenating key=value pairs
            var queryBuilder = new StringBuilder();
            var hashBuilder = new StringBuilder();

            foreach (var kv in sorted)
            {
                if (string.IsNullOrEmpty(kv.Value)) continue;

                var encodedValue = Uri.EscapeDataString(kv.Value);
                if (queryBuilder.Length > 0)
                {
                    queryBuilder.Append('&');
                    hashBuilder.Append('&');
                }
                queryBuilder.Append($"{kv.Key}={encodedValue}");
                hashBuilder.Append($"{kv.Key}={kv.Value}"); // hash uses raw value or encoded? VNPay expects raw before URL-encoding in many examples
            }

            // 3. Compute HMAC SHA512 of the hash data string
            var secureHash = HmacSha512(hashSecret.ToString(), queryBuilder.ToString());

            // 4. Append vnp_SecureHash to query
            queryBuilder.Append($"&vnp_SecureHash={Uri.EscapeDataString(secureHash)}");

            // 5. Return full URL
            return $"{baseUrl}?{queryBuilder}";
        }

        /// <summary>
        /// Validate signature present in query params
        /// </summary>
        public static bool ValidateSignature(IDictionary<string, string> query, string hashSecret)
        {
            // VNPay returns vnp_SecureHash and vnp_SecureHashType
            if (!query.TryGetValue("vnp_SecureHash", out var receivedHash)) return false;

            // Build hashData from query params excluding vnp_SecureHash/vnp_SecureHashType
            var filtered = query
                .Where(k => k.Key != "vnp_SecureHash" && k.Key != "vnp_SecureHashType")
                .OrderBy(k => k.Key, StringComparer.Ordinal);

            var hashBuilder = new StringBuilder();
            foreach (var kv in filtered)
            {
                if (string.IsNullOrEmpty(kv.Value)) continue;
                if (hashBuilder.Length > 0) hashBuilder.Append('&');
                hashBuilder.Append($"{kv.Key}={kv.Value}");
            }

            var computed = HmacSha512(hashSecret, hashBuilder.ToString());

            // VNPay uses uppercase for hex sometimes; compare case-insensitive
            return string.Equals(receivedHash, computed, StringComparison.OrdinalIgnoreCase);
        }

        private static string HmacSha512(string key, string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using var hmac = new HMACSHA512(keyBytes);
            var hashed = hmac.ComputeHash(dataBytes);
            return BitConverter.ToString(hashed).Replace("-", "").ToLowerInvariant();
        }
    }
}
