using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.FarmSpecs
{
    public sealed class FilterMallFarmSpecification : BaseSpecification<Farm>
    {
        public FilterMallFarmSpecification()
        {
            ApplyCriteria(f => f.CertificateUrl != null && !f.IsDelete);
        }
    }
}
