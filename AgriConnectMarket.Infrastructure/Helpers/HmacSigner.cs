using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Helpers
{
    public static class HmacSigner
    {
        public static string Sign(string message, string secret)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            using var h = new HMACSHA256(key);
            var bytes = Encoding.UTF8.GetBytes(message);
            var hash = h.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool Verify(string message, string secret, string signature)
        {
            var expected = Sign(message, secret);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(expected),
                Convert.FromBase64String(signature));
        }
    }
}
