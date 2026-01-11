using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Normalization;

namespace AgriConnectMarket.Domain.Entities
{
    public class Farm : BaseEntity<Guid>, IAuditableEntity
    {
        public string FarmName { get; set; }
        public string? FarmDesc { get; set; }
        public string? BatchCodePrefix { get; set; }
        public string? BannerUrl { get; set; }
        public string? CertificateUrl { get; private set; }
        public string? Phone { get; set; }
        public string? Area { get; set; }
        public bool IsDelete { get; set; }
        public bool IsBanned { get; set; }
        public bool IsValidForSelling { get; set; }
        public bool IsConfirmAsMall { get; private set; }


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
        public ICollection<FavoriteFarm> FavoriteFarms { get; set; }
        public virtual ICollection<ViolationReport> ViolationReports { get; set; }

        public Farm() { }

        public Farm(string farmName, string? farmDesc, string batchCodePrefix, string? bannerUrl, string phone, string area, Guid farmerId)
        {
            Guard.AgainstNullOrWhiteSpace(farmName, nameof(farmName));
            Guard.AgainstNullOrWhiteSpace(batchCodePrefix, nameof(batchCodePrefix));
            Guard.AgainstNullOrWhiteSpace(bannerUrl, nameof(bannerUrl));
            Guard.AgainstNullOrWhiteSpace(phone, nameof(phone));
            Guard.AgainstNullOrEmpty(area, nameof(area));
            Guard.AgainstNull(farmerId, nameof(farmerId));

            FarmName = farmName;
            FarmDesc = farmDesc;
            BatchCodePrefix = Normalizer.NormalizeStringToUpper(batchCodePrefix);
            BannerUrl = Normalizer.NormalizeUrl(bannerUrl ?? string.Empty);
            Phone = Normalizer.NormalizePhone(phone);
            Area = area;
            FarmerId = farmerId;
            IsDelete = false;
            IsBanned = false;
            IsConfirmAsMall = false;
            IsValidForSelling = false;
        }

        public void ToggleFarmBanned()
        {
            this.IsBanned = !this.IsBanned;
        }

        public void AllowSelling()
        {
            if (string.IsNullOrEmpty(this.FarmDesc)
                || string.IsNullOrEmpty(this.BannerUrl)
                || string.IsNullOrEmpty(this.Area)
                || string.IsNullOrEmpty(this.Phone))
            {
                throw new InvalidOperationException(MessageConstant.FARM_MISSING_INFO_FOR_SELL);
            }

            this.IsValidForSelling = true;
        }

        public void ConfirmMallFarm()
        {
            if (string.IsNullOrEmpty(this.CertificateUrl))
            {
                throw new InvalidOperationException(MessageConstant.FARM_MISSING_CERT);
            }

            if (this.IsBanned || this.IsDelete)
            {
                throw new InvalidOperationException(MessageConstant.FARM_BANNED_OR_DELETED);
            }

            this.IsConfirmAsMall = true;
        }

        public void UpdateCertificateUrl(string url)
        {
            this.CertificateUrl = url;
            this.IsConfirmAsMall = false;
        }
    }
}
