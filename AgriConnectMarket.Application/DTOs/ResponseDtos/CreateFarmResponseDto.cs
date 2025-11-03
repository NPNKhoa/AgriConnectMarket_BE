namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateFarmResponseDto
    {
        public Guid FarmId { get; set; }
        public string FarmName { get; set; }
        public string? FarmDesc { get; set; }
        public string? BannerUrl { get; set; }
        public string Phone { get; set; }
        public string Area { get; set; }
    }
}
