namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public string ProductAttribute { get; set; }
        public string? ProductDesc { get; set; }
        public Guid CategoryId { get; set; }
    }
}
