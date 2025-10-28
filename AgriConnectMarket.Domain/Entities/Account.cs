using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Account : BaseEntity<Guid>, IAuditableEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = ROLE.BUYER;
        public bool IsActive { get; set; }
        public bool IsDeLeted { get; set; }

        // Auditable properties
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }


        public Profile Profile { get; set; }
        public Farm Farm { get; set; }

        public Account() { }
        public Account(string username, string password)
        {
            Guard.AgainstNullOrWhiteSpace(username, nameof(username));
            Guard.AgainstNullOrWhiteSpace(password, nameof(password));

            UserName = username;
            Password = password;
        }
    }
}
