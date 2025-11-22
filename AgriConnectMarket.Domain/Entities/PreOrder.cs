using AgriConnectMarket.SharedKernel.Guards;

namespace AgriConnectMarket.Domain.Entities
{
    public class PreOrder
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpectedReleaseDate { get; set; }
        public decimal? PartiallyPaidAmount { get; set; }
        public string? Note { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        public PreOrder()
        {

        }

        private PreOrder(Order order, Guid productId, decimal quantity, DateTime expectedReleaseDate, string note = "")
        {
            OrderId = order.Id;
            ProductId = productId;
            Quantity = quantity;
            ExpectedReleaseDate = expectedReleaseDate;
            Note = note;

            Order = order;
        }

        public static PreOrder Create(Order order, Guid productId, decimal quantity, DateTime expectedReleaseDate, string note = "")
            => new PreOrder(order, productId, quantity, expectedReleaseDate, note);

        public void UpdatePartiallyPaidAmount(decimal partiallyAmount)
        {
            Guard.AgainstNegative(partiallyAmount, nameof(partiallyAmount));

            PartiallyPaidAmount = partiallyAmount;
        }
    }
}
