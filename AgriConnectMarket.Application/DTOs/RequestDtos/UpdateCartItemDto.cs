namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class UpdateCartItemDto
    {
        public Guid BatchId { get; set; }
        public int Quantity { get; set; }
    }
}
