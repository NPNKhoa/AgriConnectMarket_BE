using AgriConnectMarket.Application.Interfaces;

namespace AgriConnectMarket.WebApi.Middlewares
{
    public sealed class TokenParsingMiddleware(RequestDelegate _next, ILogger<TokenParsingMiddleware> _logger, IServiceScopeFactory _scopeFactory)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                using var scope = _scopeFactory.CreateScope();
                var tokenValidator = scope.ServiceProvider.GetRequiredService<ITokenValidator>();

                var principal = tokenValidator.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }

            await _next(context);
        }
    }
}

