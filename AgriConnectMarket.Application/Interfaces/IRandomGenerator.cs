namespace AgriConnectMarket.Application.Interfaces
{
    public interface IRandomGenerator
    {
        string GenerateNumeric(int length);
        string GenerateAlphaNumeric(int length);
    }
}
