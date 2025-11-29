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
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }

        // Navigation
        public Guid SeasonId { get; set; }
        public Season Season { get; set; }

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

        private ProductBatch(decimal totalYield, decimal availableQuantity,
            string units, DateTime plantingDate, Guid seasonId, decimal price)
        {
            TotalYield = totalYield;
            AvailableQuantity = availableQuantity;
            Units = units;
            PlantingDate = plantingDate;
            SeasonId = seasonId;
            HarvestDate = DateTime.UtcNow;
            Price = price;
        }

        public static ProductBatch Create(Guid seasonId, decimal totalYield, decimal availableQuantity,
            DateTime plantingDate, decimal price, string units = "kg")
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));
            Guard.AgainstNegative(totalYield, nameof(totalYield));
            Guard.AgainstNegative(availableQuantity, nameof(availableQuantity));
            Guard.AgainstNull(plantingDate, nameof(plantingDate));
            Guard.AgainstNegative(price, nameof(price));

            return new ProductBatch(totalYield, availableQuantity, units, plantingDate, seasonId, price);
        }

        public void SetBatchCode(BatchCode code)
        {
            Guard.AgainstNull(code, nameof(code));

            BatchCode = code;
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
