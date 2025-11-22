using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; private set; }

        public Order Order { get; set; }
        public ProductBatch Batch { get; set; }

        public OrderItem()
        {

        }

        private OrderItem(Guid orderId, Guid batchId, decimal unitPrice, decimal quantity)
        {
            OrderId = orderId;
            BatchId = batchId;
            UnitPrice = unitPrice;
            Quantity = quantity;

            SubTotal = unitPrice * quantity;
        }

        public static OrderItem Create(Guid orderId, Guid batchId, decimal unitPrice, decimal quantity)
            => new OrderItem(orderId, batchId, unitPrice, quantity);

        public void UpdateItem(decimal unitPrice, decimal quantity)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;

            SubTotal = unitPrice * quantity;
        }

        // HELPERS
        public void IncreasQuantity(decimal quantity) => Quantity += quantity;

        public void ReCalculateSubTotal(decimal unitPrice)
        {
            SubTotal = unitPrice * Quantity;
        }
    }
}
