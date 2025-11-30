using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductBatchSpecs
{
    public class FilterProductBatchByPaginationSpecification : BaseSpecification<ProductBatch>
    {
        public FilterProductBatchByPaginationSpecification(int skip, int take)
        {
            ApplyPaging(skip, take);
        }
    }
}
