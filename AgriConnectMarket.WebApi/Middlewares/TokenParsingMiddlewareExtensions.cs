namespace AgriConnectMarket.WebApi.Middlewares
{
    public static class TokenParsingMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenParsing(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenParsingMiddleware>();
        }
    }
}
