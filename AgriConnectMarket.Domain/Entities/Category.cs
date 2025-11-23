using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;

namespace AgriConnectMarket.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string IllustrativeImageUrl { get; set; }
        public bool IsDelete { get; set; } = false;

        public IEnumerable<Product> Products { get; set; }

        public Category() { }

        public Category(string name, string desc, string imageUrl = "")
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(desc, nameof(desc));

            CategoryName = name;
            CategoryDesc = desc;
            IllustrativeImageUrl = imageUrl;
            IsDelete = false;
        }
    }
}
