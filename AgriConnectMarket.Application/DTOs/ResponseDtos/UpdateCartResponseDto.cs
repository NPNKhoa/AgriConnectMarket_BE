namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record UpdateCartResponseDto(Guid itemId, int quantity, decimal itemPrice, decimal totalCartPrice);
}
