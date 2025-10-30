namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class LoginResultDto
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    };
}
