namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateSeasonDto
    {
        public string SeasonName { get; set; }
        public string? SeasonDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid FarmId { get; set; }
        public Guid ProductId { get; set; }
    }
}
