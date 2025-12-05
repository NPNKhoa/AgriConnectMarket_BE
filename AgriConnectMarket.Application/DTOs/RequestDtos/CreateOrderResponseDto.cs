using AgriConnectMarket.Application.DTOs.ResponseDtos;

namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record CreateOrderResponseDto(
        Guid OrderId,
        ProfileDto Customer,
        AddressDto Address,
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

        DateTime CreatedAt,
        DateTime? UpdatedAt,
        TransactionDto? Transaction,
        IReadOnlyCollection<ItemGroupedByFarmDto> OrderItems
    );
}
