using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductsSpecs
{
    public sealed class FilterProductBySearchTerm : BaseSpecification<Product>
    {
        public FilterProductBySearchTerm(string searchTerm = "")
        {
            string normalizedSearchTerm = searchTerm.Trim().ToLower();

            ApplyCriteria(p => p.ProductName.ToLower().Contains(normalizedSearchTerm)
                            || p.ProductDesc.ToLower().Contains(normalizedSearchTerm)
                            || p.ProductAttribute.ToLower().Contains(normalizedSearchTerm));
        }
    }
}
