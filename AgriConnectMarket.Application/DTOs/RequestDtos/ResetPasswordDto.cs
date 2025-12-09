namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record ResetPasswordDto(string Email, string ResetToken, string NewPassword);
}
