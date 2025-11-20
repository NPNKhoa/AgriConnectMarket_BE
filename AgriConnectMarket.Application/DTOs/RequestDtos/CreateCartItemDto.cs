namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateCartItemDto
    {
        public Guid CartId { get; set; }
        public Guid BatchId { get; set; }
        public int Quantity { get; set; }
    }
}
