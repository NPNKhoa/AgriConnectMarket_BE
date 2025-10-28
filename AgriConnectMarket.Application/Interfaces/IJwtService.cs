using System.Security.Claims;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId, string username, string role);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
