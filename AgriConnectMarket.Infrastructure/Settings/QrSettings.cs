namespace AgriConnectMarket.Infrastructure.Settings
{
    public class QrSettings
    {
        public string Secret { get; init; } = default!;
        public string BaseUrl { get; init; } = default!;
        public string Issuer { get; init; } = "AgriConnectMarket";
    }
}
