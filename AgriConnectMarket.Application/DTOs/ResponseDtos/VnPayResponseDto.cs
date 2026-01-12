namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record VnPayResponseDto(string responseCode, string orderCode, string returnUrl = "http://localhost:5173/payment-result");
}
