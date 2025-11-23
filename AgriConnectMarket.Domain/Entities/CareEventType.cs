using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class CareEventType : BaseEntity<Guid>
    {
        public string EventTypeName { get; set; }
        public string EventTypeDesc { get; set; }

        public ICollection<CareEvent> CareEvents { get; set; }

        public CareEventType()
        {

        }

        private CareEventType(string name, string description)
        {
            EventTypeName = name;
            EventTypeDesc = description;
        }

        public static CareEventType Create(string name, string description)
            => new CareEventType(name, description);
    }
}
