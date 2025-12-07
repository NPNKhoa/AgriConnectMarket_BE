using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Specifications;
using System.Linq.Expressions;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterOrderItemInFarmAndPaidOrderSpecification : BaseSpecification<OrderItem>
    {
        public FilterOrderItemInFarmAndPaidOrderSpecification(Guid farmId)
        {
            ApplyCriteria(i => i.Order.PaymentStatus == PaymentStatusConst.PAID);

            ApplyCriteria(i => i.Batch.Season.FarmId == farmId);

            AddIncludeChain(
                (Expression<Func<OrderItem, ProductBatch>>)(i => i.Batch),
                (Expression<Func<ProductBatch, Season>>)(b => b.Season),
                (Expression<Func<Season, Product>>)(s => s.Product)
            );
        }
    }
}
