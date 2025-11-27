using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class Order : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal ShippingFee { get; set; }
        public string OrderStatus { get; set; } = OrderStatusEnum.PENDING;
        public string OrderType { get; set; } = OrderTypeConst.ORDER;
        public string PaymentStatus { get; set; } = PaymentStatusConst.PENDING;
        public DateTime? PaidDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Profile Customer { get; set; }
        public PreOrder PreOrder { get; set; }
        public Address Address { get; set; }

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public Order()
        {

        }

        private Order(Guid customerId, Guid addressId, string orderCode, DateTime orderDate, decimal shippingFee = 0, string? orderType = "")
        {
            CustomerId = customerId;
            AddressId = addressId;
            OrderCode = orderCode;
            TotalPrice = 0;
            OrderDate = orderDate;
            OrderType = string.IsNullOrEmpty(orderType) ? OrderTypeConst.ORDER : orderType;
            ShippingFee = OrderType == OrderTypeConst.PREORDER ? 0 : shippingFee;

            OrderStatus = OrderStatusEnum.PENDING;
            PaymentStatus = PaymentStatusConst.PENDING;
        }

        public static Order Create(Guid customerId, Guid addressId, string orderCode, DateTime orderDate, string orderType = "", decimal shippingFee = 0)
            => new Order(customerId, addressId, orderCode, orderDate, shippingFee, orderType);

        public void AddItem(ProductBatch batch, decimal quantity)
        {
            Guard.AgainstNegative(quantity, nameof(quantity));

            var item = OrderItem.Create(Id, batch.Id, batch.Price, quantity);

            var existing = _orderItems.FirstOrDefault(item => item.BatchId == batch.Id);

            if (existing is not null)
            {
                existing.IncreasQuantity(quantity);
                existing.ReCalculateSubTotal(batch.Price);
            }
            else
            {
                _orderItems.Add(item);
            }

            ReCalculateTotal();
        }

        public void UpdateOrderStatus(string newStatus, DateTime? deliveredDate = null)
        {
            Guard.AgainstInvalidEnumValue(typeof(OrderStatusEnum), newStatus, nameof(newStatus));

            if (newStatus.Equals(OrderStatusEnum.DELIVERED) && deliveredDate is null)
            {
                Guard.AgainstNull(deliveredDate, nameof(deliveredDate));
            }

            OrderStatus = newStatus;
            DeliveredDate = deliveredDate;
        }

        public void UpdatePaymentStatus(string newStatus, DateTime? paidDate)
        {
            if (newStatus.Equals(PaymentStatusConst.PAID) && paidDate is null)
            {
                Guard.AgainstNull(paidDate, nameof(paidDate));
            }

            PaymentStatus = newStatus;
            PaidDate = paidDate;
        }

        // HELPERS
        protected void ReCalculateTotal()
        {
            TotalPrice = _orderItems.Sum(i => i.SubTotal);
        }
    }
}
