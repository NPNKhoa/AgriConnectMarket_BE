namespace AgriConnectMarket.Application.Interfaces
{
    public interface IOrderCodeGenerator
    {
        private const string Prefix = "ORD";
        string GenerateOrderCode(string? prefix = Prefix);
    }
}
