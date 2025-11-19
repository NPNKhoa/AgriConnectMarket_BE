using AgriConnectMarket.Application.Interfaces;
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
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                    return null;

                var idValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(idValue, out var guid) ? guid : null;
            }
        }
        public string? Username => _principal?.Identity?.Name;
        public IEnumerable<string> Roles => _principal?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();

        public void SetClaims(ClaimsPrincipal principal) => _principal = principal;
    }
}
