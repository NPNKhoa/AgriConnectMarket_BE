using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Normalization;

namespace AgriConnectMarket.Domain.Entities
{
    public class Farm : BaseEntity<Guid>, IAuditableEntity
    {
        public string FarmName { get; set; }
        public string? BannerUrl { get; set; }
        public string? CertificateUrl { get; set; }
        public string Phone { get; set; }
        public string Area { get; set; }


        // Auditable properties
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Aggregatred properties
        public Guid FarmerId { get; set; }
        public Account Farmer { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Season> Seasons { get; set; }

        public Farm() { }
        public Farm(string farmName, string? bannerUrl, string phone, string area, Guid farmerId, Guid addressId)
        {
            Guard.AgainstNullOrWhiteSpace(farmName, nameof(farmName));
            Guard.AgainstNullOrWhiteSpace(bannerUrl, nameof(bannerUrl));
            Guard.AgainstNullOrWhiteSpace(phone, nameof(phone));
            Guard.AgainstNullOrEmpty(area, nameof(area));
            Guard.AgainstNull(farmerId, nameof(farmerId));
            Guard.AgainstNull(addressId, nameof(addressId));

            this.FarmName = farmName;
            this.BannerUrl = Normalizer.NormalizeUrl(bannerUrl);
            this.Phone = Normalizer.NormalizePhone(phone);
            this.Area = area;
            this.FarmerId = farmerId;
            this.AddressId = addressId;
        }
    }
}
