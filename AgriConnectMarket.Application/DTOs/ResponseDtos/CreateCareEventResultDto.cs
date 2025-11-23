using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateCareEventResultDto
    {
        public DateTime OccurredAt { get; set; }
        public string Payload { get; set; }

        public CareEventType EventType { get; set; }
        public ProductBatch Batch { get; set; }
    }
}
