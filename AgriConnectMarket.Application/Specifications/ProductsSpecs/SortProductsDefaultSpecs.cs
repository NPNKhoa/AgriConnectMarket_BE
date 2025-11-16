using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductsSpecs
{
    public sealed class SortProductsDefaultSpecs : BaseSpecification<Product>
    {
        public SortProductsDefaultSpecs()
        {
            ApplyOrderByDescending(p => p.CreatedAt);
        }
    }
}
