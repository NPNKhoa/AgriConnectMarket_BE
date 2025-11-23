using AgriConnectMarket.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class Sha256Hashing : IHashingStrategy
    {
        public string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public string BuildCareEventCanonical(string batchId, string eventKey, string dataJson, string occuredAt, string previousHash)
        {
            return $"{batchId}|{eventKey}|{dataJson}|{occuredAt}|{previousHash}";
        }
    }
}
