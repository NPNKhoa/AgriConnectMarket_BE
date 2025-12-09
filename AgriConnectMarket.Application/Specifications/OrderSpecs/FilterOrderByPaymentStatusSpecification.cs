using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Normalization;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.OrderSpecs
{
    public class FilterOrderByPaymentStatusSpecification : BaseSpecification<Order>
    {
        public FilterOrderByPaymentStatusSpecification(string paymentMethod)
        {
            Guard.AgainstInvalidEnumValue(typeof(PaymentMethodConst), paymentMethod, nameof(paymentMethod));

            ApplyCriteria(o => o.PaymentMethod.ToUpper().Equals(Normalizer.NormalizeStringToUpper(paymentMethod)));

            ApplyOrderByDescending(o => o.CreatedAt);
        }
    }
}
