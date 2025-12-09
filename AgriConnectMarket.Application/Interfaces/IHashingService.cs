namespace AgriConnectMarket.Application.Interfaces
{
    public interface IHashingService
    {
        (string HashedValue, string Salt) Hash(string value);
        bool Verify(string value, string hashedValue, string salt);
    }
}
