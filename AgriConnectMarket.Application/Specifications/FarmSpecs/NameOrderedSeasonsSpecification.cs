using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public class NameOrderedSeasonsSpecification : BaseSpecification<Season>
    {
        public NameOrderedSeasonsSpecification()
        {
            ApplyOrderBy(f => f.SeasonName);
        }
    }
}
