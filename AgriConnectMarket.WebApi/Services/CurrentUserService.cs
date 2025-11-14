using AgriConnectMarket.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AgriConnectMarket.WebApi.Services
{
    public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
    {
        private ClaimsPrincipal? _principal;

        public bool IsAuthenticated => _principal?.Identity?.IsAuthenticated ?? false;
        public Guid? UserId
        {
            get
            {
                var id = _principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? _principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
                return Guid.TryParse(id, out var g) ? g : (Guid?)null;
            }
        }
        public string? Username => _principal?.Identity?.Name;
        public IEnumerable<string> Roles => _principal?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();

        public void SetClaims(ClaimsPrincipal principal) => _principal = principal;
    }
}
