using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public sealed class FilterFarmBySearchTermSpecification : BaseSpecification<Farm>
    {
        public FilterFarmBySearchTermSpecification(string searchTerm)
        {
            ApplyCriteria(f => !f.IsDelete && (f.FarmName == searchTerm || f.Area == searchTerm));
        }
    }
}
