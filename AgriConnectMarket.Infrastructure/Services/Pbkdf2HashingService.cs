using AgriConnectMarket.Application.Interfaces;
using System.Security.Cryptography;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class Pbkdf2HashingService : IHashingService
    {
        private const int Iterations = 100_000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public (string HashedValue, string Salt) Hash(string value)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(value, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
        }

        public bool Verify(string value, string hashedValue, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(value, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, Convert.FromBase64String(hashedValue));
        }
    }

}
