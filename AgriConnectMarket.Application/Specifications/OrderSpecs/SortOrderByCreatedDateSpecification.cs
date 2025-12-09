using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class SortOrderByCreatedDateSpecification : BaseSpecification<Order>
    {
        public SortOrderByCreatedDateSpecification()
        {
            ApplyOrderByDescending(o => o.CreatedAt);
        }
    }
}
