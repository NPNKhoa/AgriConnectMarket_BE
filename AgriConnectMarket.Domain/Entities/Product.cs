using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Product : BaseEntity<Guid>, IAuditableEntity
    {
        public string ProductName { get; set; }
        public string ProductAttribute { get; set; }
        public string? ProductDesc { get; set; }
        // And more...

        // Navigations
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Season Season { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Product()
        {

        }

        public Product(string productName, string productAttribute, string? productDesc = null)
        {
            Guard.AgainstNullOrEmpty(productName, nameof(productName));
            Guard.AgainstNullOrEmpty(productAttribute, nameof(productAttribute));

            ProductName = productName;
            ProductAttribute = productAttribute;
            ProductDesc = productDesc;
        }
    }
}
