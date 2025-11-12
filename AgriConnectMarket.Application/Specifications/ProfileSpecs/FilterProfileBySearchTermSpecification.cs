using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProfileSpecs
{
    public sealed class FilterProfileBySearchTermSpecification : BaseSpecification<Profile>
    {
        public FilterProfileBySearchTermSpecification(string searchTerm)
        {
            ApplyCriteria(f =>
                f.Fullname.ToLower().Contains(searchTerm.ToLower())
                || f.Email.ToLower().Contains(searchTerm.ToLower())
                || f.Phone.Contains(searchTerm)
            );
        }
    }
}
