using AgriConnectMarket.Domain.ValueObjects;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class ProductBatch : BaseEntity<Guid>, IAuditableEntity
    {
        public BatchCode BatchCode { get; set; }
        public decimal TotalYield { get; set; }
        public decimal AvailableQuantity { get; set; }
        public string Units { get; set; }
        public decimal Price { get; set; }
        public string? VerificationQr { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime? HarvestDate { get; set; }

        // Navigation
        public Guid SeasonId { get; set; }
        public Season Season { get; set; }

        public virtual ICollection<PreOrder> PreOrders { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        private readonly List<ProductBatchImage> _imageUrls = new();
        public virtual IReadOnlyCollection<ProductBatchImage> ImageUrls => _imageUrls.AsReadOnly();
        // order item, cart item, review...

        // Audit
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public ProductBatch() { }

        private ProductBatch(decimal totalYield, string units, DateTime plantingDate, Guid seasonId)
        {
            TotalYield = totalYield;
            Units = units;
            PlantingDate = plantingDate;
            SeasonId = seasonId;

            AvailableQuantity = 0;
            VerificationQr = null;
            HarvestDate = null;
            Price = 0;
        }

        public static ProductBatch Create(Guid seasonId, decimal totalYield, DateTime plantingDate, string units = "kg")
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));
            Guard.AgainstNegative(totalYield, nameof(totalYield));
            Guard.AgainstNull(plantingDate, nameof(plantingDate));

            return new ProductBatch(totalYield, units, plantingDate, seasonId);
        }

        public void SetBatchCode(BatchCode code)
        {
            Guard.AgainstNull(code, nameof(code));

            BatchCode = code;
        }

        public void Harvest(DateTime currentDateTimeUtc, decimal totalYield)
        {
            Guard.AgainstNull(currentDateTimeUtc, nameof(currentDateTimeUtc));
            Guard.AgainstNegative(totalYield, nameof(totalYield));

            this.HarvestDate = currentDateTimeUtc;
            this.TotalYield = totalYield;
        }

        public void Sell(decimal availableQuantity, decimal price)
        {
            Guard.AgainstOutOfRange(availableQuantity, 0, this.TotalYield, nameof(availableQuantity));
            Guard.AgainstNegative(price, nameof(price));

            this.AvailableQuantity = availableQuantity;
            this.Price = price;
        }

        public void UpdateInventory(decimal soldQuantity)
        {
            Guard.AgainstNegative(soldQuantity, nameof(AvailableQuantity));

            decimal availableQuantity = TotalYield - soldQuantity;

            Guard.AgainstOutOfRange<decimal>(availableQuantity, 0, TotalYield, nameof(AvailableQuantity));

            AvailableQuantity = availableQuantity;
        }

        public void AddImage(ProductBatchImage image)
        {
            _imageUrls.Add(image);
        }
    }
}
