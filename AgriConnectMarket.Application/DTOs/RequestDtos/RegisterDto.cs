namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record RegisterDto
    (
        string Username,
        string Password,
        string Email,
        string Phone,
        string Fullname,
        string? AvatarUrl
    );
}
