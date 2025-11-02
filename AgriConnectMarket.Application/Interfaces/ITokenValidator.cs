using System.Security.Claims;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ITokenValidator
    {
        /// <summary>
        /// Validate the raw token string. Returns a ClaimsPrincipal when valid, otherwise null.
        /// Should not throw for expected validation failures; return null or a Result type.
        /// </summary>
        /// <param name="token">Raw token (without "Bearer ").</param>
        /// <returns>ClaimsPrincipal if valid; otherwise null.</returns>
        ClaimsPrincipal? ValidateToken(string token);
    }
}
