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

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public IEnumerable<ProductBatch> ProductBatches { get; set; }

        public Season()
        {

        }

        public Season(string seasonName, string? seasonDesc, DateTime startDate, DateTime endDate, Guid farmId, Guid productId, string status = SeasonStatusEnums.PENDING)
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
            this.FarmId = farmId;
            this.ProductId = productId;
        }

        public void UpdateStatus(string newStatus)
        {
            Guard.AgainstInvalidEnumValue(typeof(SeasonStatusEnums), newStatus, nameof(newStatus));

            this.Status = newStatus;

            switch (newStatus)
            {
                case SeasonStatusEnums.CLOSED:
                case SeasonStatusEnums.HARVESTED:
                    this.EndDate = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }
    }
}
