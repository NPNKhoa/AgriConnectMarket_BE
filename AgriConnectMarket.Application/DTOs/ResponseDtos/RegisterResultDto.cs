namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class RegisterResultDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
    }
}
