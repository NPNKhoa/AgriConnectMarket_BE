using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductAttribute { get; set; }
        public string? ProductDesc { get; set; }
        public Category Category { get; set; }
    }
}
