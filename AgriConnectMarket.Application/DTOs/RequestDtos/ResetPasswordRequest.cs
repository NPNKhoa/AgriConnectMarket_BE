namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record ResetPasswordRequest(string EmailOrPhone, string ResetToken, string NewPassword);
}
