namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record PreOrderDto(
       Guid OrderId,
       decimal Quantity,
       DateTime? ExpectedReleaseDate,
       string? Note,
       Guid CustomerId,
       Guid AddressId,
       string OrderCode,
       decimal TotalPrice,
       DateTime OrderDate,
       decimal ShippingFee,
       string OrderStatus,
       string OrderType,
       string PaymentStatus,
       string PaymentMethod,
       DateTime? PaidDate,
       DateTime? DeliveredDate,
       DateTime CreatedAt
   );
}
