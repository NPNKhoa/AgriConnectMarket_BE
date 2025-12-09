using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class InlcudeOrderItemAddressInOrderSpecification : BaseSpecification<Order>
    {
        public InlcudeOrderItemAddressInOrderSpecification()
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.Customer);
            AddInclude(o => o.Address);

            ApplyOrderByDescending(o => o.CreatedAt);
        }
    }
}
