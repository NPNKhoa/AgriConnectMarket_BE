using AgriConnectMarket.SharedKernel.Guards;

namespace AgriConnectMarket.Domain.Entities
{
    public class PreOrder
    {
        public Guid OrderId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? ExpectedReleaseDate { get; private set; } // The date that the farmer expect to deliver this pro-order
        public string? Note { get; set; }

        public Order Order { get; set; }
        public ProductBatch Batch { get; set; }

        public PreOrder()
        {

        }

        private PreOrder(Order order, Guid productId, decimal quantity, string note = "")
        {
            OrderId = order.Id;
            BatchId = productId;
            Quantity = quantity;
            ExpectedReleaseDate = null;
            Note = note;

            Order = order;
        }

        public static PreOrder Create(Order order, Guid batchId, decimal quantity, string note = "")
            => new PreOrder(order, batchId, quantity, note);

        public void Approve(DateTime releaseDate)
        {
            Guard.AgainstNull(releaseDate, nameof(releaseDate));

            this.ExpectedReleaseDate = releaseDate;
        }
    }
}
