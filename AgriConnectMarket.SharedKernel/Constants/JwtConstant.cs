namespace AgriConnectMarket.SharedKernel.Constants
{
    public static class JwtConstants
    {
        public const string AuthorizationHeader = "Authorization";
        public const string BearerPrefix = "Bearer ";
        public const string UserIdClaim = "sub"; // or "uid" depending on your token
    }
}
