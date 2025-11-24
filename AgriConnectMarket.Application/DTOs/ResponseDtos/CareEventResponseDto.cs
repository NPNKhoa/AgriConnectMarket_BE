namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CareEventResponseDto
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public Guid EventTypeId { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Payload { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
    }
}
