using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterPaidOrderByFarmSpecification : BaseSpecification<Order>
    {
        public FilterPaidOrderByFarmSpecification(Guid farmId)
        {
            AddInclude(o => o.Customer);

            ApplyCriteria(o =>
                o.PaymentStatus == PaymentStatusConst.PAID
                    && o.OrderItems.Any(
                        i => i.Batch.Season.FarmId == farmId
                    )
            );
        }
    }
}
