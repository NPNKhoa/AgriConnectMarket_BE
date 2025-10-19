using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Normalization;

namespace AgriConnectMarket.Domain.Entities
{
    public class Profile : BaseEntity<Guid>
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }

        public Profile(string fullname, string Username, string phone, string avatarUrl)
        {
            Guard.AgainstNullOrEmpty(fullname, nameof(fullname));
            Guard.AgainstNullOrEmpty(Username, nameof(Username));
            Guard.AgainstNullOrEmpty(phone, nameof(phone));
            Guard.AgainstNullOrEmpty(avatarUrl, nameof(avatarUrl));

            // basic normalization
            Username = Normalizer.NormalizeUsername(Username);
            Phone = Normalizer.NormalizePhone(phone);
            AvatarUrl = Normalizer.NormalizeUrl(avatarUrl);
        }
    }
}
