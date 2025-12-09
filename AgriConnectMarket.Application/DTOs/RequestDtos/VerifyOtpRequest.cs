namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record VerifyOtpRequest(string EmailOrPhone, string Otp);
}
