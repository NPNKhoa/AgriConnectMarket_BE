namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; }
        public string ProductAttribute { get; set; }
        public string? ProductDesc { get; set; }
        public Guid CategoryId { get; set; }
    }
}
