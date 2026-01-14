namespace AgriConnectMarket.Application.Interfaces
{
    public interface IHashingStrategy
    {
        public string ComputeHash(string input);
        public string BuildCareEventCanonical(string batchId, string eventKey, string dataJson, string previousHash);
    }
}
