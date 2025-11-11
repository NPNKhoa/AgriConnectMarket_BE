using AgriConnectMarket.Application.Interfaces;
using System.Security.Claims;

namespace AgriConnectMarket.WebApi.Services
{
    public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
    {
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

        public string? Username
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                return user?.FindFirstValue(ClaimTypes.Name);
            }
        }
    }
}
