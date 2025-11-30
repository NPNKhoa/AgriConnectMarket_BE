using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductBatchSpecs
{
    public class FilterProductBatchByCategoryIdSpecification : BaseSpecification<ProductBatch>
    {
        public FilterProductBatchByCategoryIdSpecification(Guid categoryId = default)
        {
            ApplyCriteria(b => b.Season.Product.CategoryId.Equals(categoryId));
        }
    }
}
