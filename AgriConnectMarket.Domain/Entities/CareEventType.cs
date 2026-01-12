using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class CareEventType : BaseEntity<Guid>
    {
        public string EventTypeName { get; set; }
        public string EventTypeDesc { get; set; }
        public string? PayloadFields { get; set; }

        public ICollection<CareEvent> CareEvents { get; set; }

        public CareEventType()
        {

        }

        private CareEventType(string name, string description)
        {
            EventTypeName = name;
            EventTypeDesc = description;
        }

        public CareEventType(Guid id, string name, string description, string fields)
        {
            Id = id;
            EventTypeName = name;
            EventTypeDesc = description;
            PayloadFields = fields;
        }

        public static CareEventType Create(string name, string description)
            => new CareEventType(name, description);
    }
}
