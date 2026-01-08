using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Payment
{
    public static class VnPayHelper
    {
        // Build VNPay URL from base url, secret and parameters
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
            //var dataBytes = Encoding.UTF8.GetBytes(queryBuilder.ToString());
            //var keyBytes = Encoding.UTF8.GetBytes(hashSecret.ToString());
            //using var hmac = new HMACSHA512(keyBytes);
            //var secureHash = BitConverter.ToString(hmac.ComputeHash(dataBytes)).Replace("-", string.Empty);
            var secureHash = HmacSha512(hashSecret.ToString(), queryBuilder.ToString());

            // 4. Append vnp_SecureHash to query
            queryBuilder.Append($"&vnp_SecureHash={Uri.EscapeDataString(secureHash)}");

            // 5. Return full URL
            return $"{baseUrl}?{queryBuilder}";
        }

        // Validate signature present in query params
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
