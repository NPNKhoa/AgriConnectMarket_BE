namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class UpdateOrderStatusResponseDto
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
