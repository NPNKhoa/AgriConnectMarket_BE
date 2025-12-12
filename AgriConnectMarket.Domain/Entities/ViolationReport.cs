using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class ViolationReport : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid FarmId { get; set; }
        public Guid CustomerId { get; set; }
        public string ViolationType { get; set; }
        public string ReportContent { get; set; }
        public string? EvidenceUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Farm Farm { get; set; }
        public Profile Customer { get; set; }

        public ViolationReport()
        {

        }

        private ViolationReport(Guid farmId, Guid customerId, string violationType, string reportContent, string evidenceUrl = "")
        {
            FarmId = farmId;
            CustomerId = customerId;
            ViolationType = violationType;
            ReportContent = reportContent;
            EvidenceUrl = evidenceUrl;
        }

        public static ViolationReport Create(Guid farmId, Guid customerId, string violationType, string reportContent, string evidenceUrl = "")
            => new ViolationReport(farmId, customerId, violationType, reportContent, evidenceUrl);
    }
}
