namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateCareEventDto
    {
        public Guid BatchId { get; set; }
        public Guid EventTypeId { get; set; }
        public string Payload { get; set; }
    }
}
