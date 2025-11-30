using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductBatchSpecs
{
    public class SortingProductBatchSpecification : BaseSpecification<ProductBatch>
    {
        public SortingProductBatchSpecification(bool isDesc = false)
        {
            if (isDesc)
            {
                ApplyOrderByDescending(b => b.CreatedAt);
            }
            else
            {
                ApplyOrderBy(b => b.CreatedAt);
            }
        }
    }
}
