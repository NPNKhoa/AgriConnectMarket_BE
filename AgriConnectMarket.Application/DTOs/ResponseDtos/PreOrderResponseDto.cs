using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record PreOrderResponseDto(
        Guid OrderId,
        string OrderCode,
        decimal Quantity,
        string? Note,
        Profile Customer,
        Address Address,
        DateTime OrderDate,
        string OrderStatus,
        string OrderType
    );
}
