namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class OrderItemDto
    {
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
    }
}
