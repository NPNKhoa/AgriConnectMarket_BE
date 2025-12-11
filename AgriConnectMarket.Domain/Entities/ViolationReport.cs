using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class ViolationReport : BaseEntity<Guid>
    {
        public Guid FarmId { get; set; }
        public Guid CustomerId { get; set; }
        public string ViolationType { get; set; }
        public string ReportContent { get; set; }
        public string? EvidenceUrl { get; set; }
    }
}
