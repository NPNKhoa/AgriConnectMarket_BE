using AgriConnectMarket.Application.Interfaces;
using System.Security.Cryptography;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class OrderCodeGenerator : IOrderCodeGenerator
    {
        private const string Prefix = "ORD";

        public string GenerateOrderCode(string? prefix = Prefix)
        {
            var ts = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var suffix = RandomAlphaNumeric(4);
            return $"{prefix}-{ts}-{suffix}";
        }


        private static string RandomAlphaNumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            using var rng = RandomNumberGenerator.Create();

            var buffer = new byte[length];
            rng.GetBytes(buffer);

            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                var idx = buffer[i] % chars.Length;
                result[i] = chars[idx];
            }

            return new string(result);
        }
    }
}
