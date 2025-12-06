using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class CareEvent : BaseEntity<Guid>
    {
        public Guid BatchId { get; set; }
        public Guid EventTypeId { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Payload { get; set; }
        public string Hash { get; private set; }
        public string PrevHash { get; private set; }
        public string? ImageUrl { get; set; }

        public CareEventType EventType { get; set; }
        public ProductBatch Batch { get; set; }

        public CareEvent()
        {

        }

        private CareEvent(Guid batchId, Guid typeId, DateTime occurredAt, string payload, string imageUrl, string hash, string prevHash)
        {
            BatchId = batchId;
            EventTypeId = typeId;
            OccurredAt = occurredAt;
            Payload = payload;
            Hash = hash;
            PrevHash = prevHash;
            ImageUrl = imageUrl;
        }

        public static CareEvent Create(Guid batchId, Guid typeId, DateTime occurredAt, string payload, string imageUrl, string hash, string prevHash)
            => new CareEvent(batchId, typeId, occurredAt, payload, imageUrl, hash, prevHash);
    }
}
