namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CareEventResponseDto
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public string EventType { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Payload { get; set; }
        public string ImageUrl { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
    }
}
