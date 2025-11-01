namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class UpdateAddressResultDto
    {
        public Guid AddressId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string? Detail { get; set; }
        public bool IsDefault { get; set; }
    }
}
