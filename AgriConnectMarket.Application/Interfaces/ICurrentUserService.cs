using System.Security.Claims;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        Guid? UserId { get; }
        string? Username { get; }
        IEnumerable<string> Roles { get; }
        void SetClaims(ClaimsPrincipal principal);
    }
}
