using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Season : BaseEntity<Guid>, IAuditableEntity
    {
        public string SeasonName { get; set; }
        public string? SeasonDesc { get; set; }
        public string Status { get; set; } = SeasonStatusEnums.PENDING;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Guid FarmId { get; set; }
        public Farm Farm { get; set; }

        public Season()
        {

        }

        public Season(string seasonName, string? seasonDesc, string status, DateTime startDate, DateTime endDate)
        {
            Guard.AgainstNullOrWhiteSpace(seasonName, nameof(seasonName));
            Guard.AgainstNullOrEmpty(status, nameof(status));
            Guard.AgainstNull(startDate, nameof(startDate));
            Guard.AgainstNull(endDate, nameof(endDate));
            Guard.AgainstInvalidEnumValue(typeof(SeasonStatusEnums), status, nameof(status));

            this.SeasonName = seasonName;
            this.SeasonDesc = seasonDesc;
            this.Status = status;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
