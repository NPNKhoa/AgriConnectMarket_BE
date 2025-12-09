using AgriConnectMarket.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        public string GenerateAlphaNumeric(int length)
        {
            throw new NotImplementedException();
        }

        public string GenerateNumeric(int length)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++) sb.Append((bytes[i] % 10).ToString());
            return sb.ToString();
        }
    }
}
