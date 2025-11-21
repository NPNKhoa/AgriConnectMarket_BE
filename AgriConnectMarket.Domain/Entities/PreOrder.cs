namespace AgriConnectMarket.Domain.Entities
{
    public class PreOrder
    {
        public Guid OrderId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpectedReleaseDate { get; set; }
        public decimal? PartiallyPaidAmount { get; set; }
        public string? Note { get; set; }

        public Order Order { get; set; }

        public PreOrder()
        {

        }

        private PreOrder(Guid orderId, Guid batchId, decimal quantity, DateTime expectedReleaseDate, decimal? partiallyPaidAmount, string note = "")
        {
            OrderId = orderId;
            BatchId = batchId;
            Quantity = quantity;
            ExpectedReleaseDate = expectedReleaseDate;
            PartiallyPaidAmount = partiallyPaidAmount;
            Note = note;
        }

        public static PreOrder Create(Guid orderId, Guid batchId, decimal quantity, DateTime expectedReleaseDate, decimal? partiallyPaidAmount, string note = "")
            => new PreOrder(orderId, batchId, quantity, expectedReleaseDate, partiallyPaidAmount, note);

    }
}
