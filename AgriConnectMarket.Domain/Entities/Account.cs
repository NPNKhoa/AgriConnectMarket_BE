using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class Account : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = RoleNames.Buyer;
        public bool IsActive { get; set; }
        public bool IsDeLeted { get; set; }
    }
}
