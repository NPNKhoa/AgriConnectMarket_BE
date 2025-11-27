namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreatePreOrderDto
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProductId { get; set; }
        public Guid FarmId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpectedReleaseDate { get; set; }
        public string? Note { get; set; }
    }
}
