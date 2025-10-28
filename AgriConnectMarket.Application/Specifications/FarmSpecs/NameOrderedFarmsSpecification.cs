using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public sealed class NameOrderedFarmsSpecification : BaseSpecification<Farm>
    {
        public NameOrderedFarmsSpecification()
        {
            ApplyOrderBy(f => f.FarmName);
        }
    }
}
