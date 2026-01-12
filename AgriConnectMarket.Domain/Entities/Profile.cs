using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Normalization;

namespace AgriConnectMarket.Domain.Entities
{
    public class Profile : BaseEntity<Guid>, IAuditableEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<FavoriteFarm> FavoriteFarms { get; set; }
        public virtual ICollection<ViolationReport> ViolationReports { get; set; }

        // Audit field
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Profile()
        {

        }

        public Profile(string fullname, string email, string phone, Guid accountId, string? avatarUrl = "")
        {
            // basic normalization
            Email = Normalizer.NormalizeEmail(email);
            Phone = Normalizer.NormalizePhone(phone);
            AvatarUrl = Normalizer.NormalizeUrl(avatarUrl!);
            Fullname = fullname;
            AccountId = accountId;
        }

        public Profile(string fullname, string email, string phone, string avatarUrl = "")
        {
            // basic normalization
            Email = Normalizer.NormalizeEmail(email);
            Phone = Normalizer.NormalizePhone(phone);
            AvatarUrl = Normalizer.NormalizeUrl(avatarUrl);
            Fullname = fullname;
        }

        public void SetProfileAvatar(string publicId, string? url = null)
        {
            AvatarUrl = url;
            CreatedAt = DateTime.UtcNow;
        }

        public void RemoveProfileImage()
        {
            AvatarUrl = null;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
