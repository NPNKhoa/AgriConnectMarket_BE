using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Normalization;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductBatchSpecs
{
    public class FilterProductBatchBySearchTermSpecification : BaseSpecification<ProductBatch>
    {
        public FilterProductBatchBySearchTermSpecification(string searchTerm = "")
        {
            string normalizedString = Normalizer.NormalizeStringToUpper(searchTerm);

            ApplyCriteria(b => b.Season.Product.ProductName.ToUpper().Contains(normalizedString) || b.BatchCode.Value.Contains(normalizedString));
        }
    }
}
