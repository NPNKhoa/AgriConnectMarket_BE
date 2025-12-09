using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreatePreOrderResponseDto
    {
        public string OrderCode { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedReleaseDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderType { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? Note { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
