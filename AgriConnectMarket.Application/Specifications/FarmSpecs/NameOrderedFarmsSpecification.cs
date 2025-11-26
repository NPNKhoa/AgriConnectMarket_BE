using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public sealed class NameOrderedFarmsSpecification : BaseSpecification<Farm>
    {
        public NameOrderedFarmsSpecification()
        {
            ApplyCriteria(f => !f.IsDelete);
            ApplyOrderBy(f => f.FarmName);
        }
    }
}
