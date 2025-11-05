using AgriConnectMarket.SharedKernel.Constants;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateSeasonResponseDto
    {
        public string SeasonName { get; set; }
        public string? SeasonDesc { get; set; }
        public string Status { get; set; } = SeasonStatusEnums.PENDING;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
