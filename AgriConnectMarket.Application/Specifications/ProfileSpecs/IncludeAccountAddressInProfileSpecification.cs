using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProfileSpecs
{
    public class IncludeAccountAddressInProfileSpecification : BaseSpecification<Profile>
    {
        public IncludeAccountAddressInProfileSpecification()
        {
            AddInclude(p => p.Account);
            AddInclude(p => p.Addresses);
        }
    }
}
