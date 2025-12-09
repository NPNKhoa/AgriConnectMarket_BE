using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Normalization;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterOrderByStatusSpec : BaseSpecification<Order>
    {
        public FilterOrderByStatusSpec(string status)
        {
            Guard.AgainstInvalidEnumValue(typeof(OrderStatusEnum), status, nameof(status));

            ApplyCriteria(o => o.OrderStatus.ToUpper().Equals(Normalizer.NormalizeStringToUpper(status)));

            ApplyOrderByDescending(o => o.CreatedAt);
        }
    }
}
