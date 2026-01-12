namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record ProcessOrderResponseDto(Guid orderId, string newStatus);
}
