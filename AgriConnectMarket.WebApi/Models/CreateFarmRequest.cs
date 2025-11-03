namespace AgriConnectMarket.WebApi.Models
{
    public class CreateFarmRequest
    {
        public string FarmName { get; set; }
        public string? FarmDesc { get; set; }
        public string Phone { get; set; }
        public string Area { get; set; }
        public Guid FarmerId { get; set; }

        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string? Detail { get; set; }

        public IFormFile FarmBanner { get; set; }
    }
}
