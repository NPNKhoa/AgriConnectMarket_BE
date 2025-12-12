namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreatePreOrderDto
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public string? Note { get; set; }
    }
}
