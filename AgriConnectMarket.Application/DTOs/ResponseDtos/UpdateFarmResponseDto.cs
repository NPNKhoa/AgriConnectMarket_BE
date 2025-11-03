namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class UpdateFarmResponseDto
    {
        public Guid FarmId { get; set; }
        public string FarmName { get; set; }
        public string? FarmDesc { get; set; }
        public string? BannerUrl { get; set; }
        public string Phone { get; set; }
        public string Area { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string? Detail { get; set; }
    }
}
