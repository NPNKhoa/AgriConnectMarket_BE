namespace AgriConnectMarket.Infrastructure.JwtServices
{
    public class JwtSettings
    {
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string Secret { get; init; } = default!;
        public int AccessTokenExpirationMinutes { get; init; }
    }
}
