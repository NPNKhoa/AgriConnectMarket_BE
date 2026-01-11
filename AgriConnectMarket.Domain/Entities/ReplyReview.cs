using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Domain.Entities
{
    public class ReplyReview : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid ReviewId { get; set; }
        public string ReplyContent { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public ReplyReview() { }

        private ReplyReview(Guid reviewId)
        {

        }
    }
}
