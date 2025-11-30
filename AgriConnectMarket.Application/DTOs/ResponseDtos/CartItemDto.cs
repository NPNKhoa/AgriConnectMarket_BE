namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CartItemDto
    {
        public Guid ItemId { get; set; }
        public Guid BatchId { get; set; }
        public string BatchCode { get; set; }
        public IReadOnlyCollection<string> BatchImageUrls { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SeasonName { get; set; }
        public decimal BatchPrice { get; set; }
        public int Quantity { get; set; }
        public string Units { get; set; }
        public decimal ItemPrice { get; set; }
        public string SeasonStatus { get; set; }
    }
}
