namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public decimal ShippingFee { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
