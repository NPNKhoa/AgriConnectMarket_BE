using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterOrderItemInFarmAndPaidOrderSpecification : BaseSpecification<OrderItem>
    {
        public FilterOrderItemInFarmAndPaidOrderSpecification(Guid farmId)
        {
            ApplyCriteria(i => i.Order.PaymentStatus == PaymentStatusConst.PAID);

            ApplyCriteria(i => i.Batch.Season.FarmId == farmId);
        }
    }
}
