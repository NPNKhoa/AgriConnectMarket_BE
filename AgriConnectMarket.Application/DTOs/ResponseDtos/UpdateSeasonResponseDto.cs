using AgriConnectMarket.SharedKernel.Constants;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class UpdateSeasonResponseDto
    {
        public string SeasonName { get; set; }
        public string? SeasonDesc { get; set; }
        public string Status { get; set; } = SeasonStatusEnums.PENDING;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
