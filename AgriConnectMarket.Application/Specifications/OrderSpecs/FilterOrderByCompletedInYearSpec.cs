using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterOrderByCompletedInYearSpec : BaseSpecification<Order>
    {
        public FilterOrderByCompletedInYearSpec(Guid farmId, string year)
        {
            ApplyCriteria(o =>
                o.OrderItems.Any(i => i.Batch.Season.FarmId == farmId) &&
                    o.PaymentStatus.Equals(PaymentStatusConst.PAID)
                        && o.CreatedAt.Year.ToString().Equals(year)
            );
        }
    }
}
