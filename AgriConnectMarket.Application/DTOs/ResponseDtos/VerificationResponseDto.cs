namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class VerificationResponseDto
    {
        public bool IsSuccess { get; set; }
        public Guid AccountId { get; set; }
        public string? Message { get; set; }
    }
}
