using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Product : BaseEntity<Guid>, IAuditableEntity
    {
        public string ProductName { get; set; }
        // And more...

        // Navigations
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Season Season { get; set; }

        // Audit
        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? UpdatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
