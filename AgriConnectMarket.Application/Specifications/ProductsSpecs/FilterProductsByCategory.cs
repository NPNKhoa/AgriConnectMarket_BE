using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductsSpecs
{
    public sealed class FilterProductsByCategory : BaseSpecification<Product>
    {
        public FilterProductsByCategory(Guid categoryId)
        {
            ApplyCriteria(p => p.CategoryId.Equals(categoryId));
        }
    }
}
