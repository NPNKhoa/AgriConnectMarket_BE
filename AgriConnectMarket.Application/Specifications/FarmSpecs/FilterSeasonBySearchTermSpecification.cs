using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public class FilterSeasonBySearchTermSpecification : BaseSpecification<Season>
    {
        public FilterSeasonBySearchTermSpecification(string searchTerm)
        {
            ApplyCriteria(f => f.SeasonName == searchTerm);
        }
    }
}
