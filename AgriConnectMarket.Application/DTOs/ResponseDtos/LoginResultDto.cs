namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class LoginResultDto
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    };
}
