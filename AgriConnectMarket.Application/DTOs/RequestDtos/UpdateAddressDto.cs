namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class UpdateAddressDto
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string? Detail { get; set; }
    }
}
