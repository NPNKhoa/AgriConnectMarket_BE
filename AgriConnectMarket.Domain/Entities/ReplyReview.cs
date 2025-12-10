using AgriConnectMarket.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Domain.Entities
{
    public class ReplyReview : BaseEntity<Guid>
    {
        public Guid ReviewId { get; set; }
        public string ReplyContent { get; set; }
        public string? ImageUrl { get; set; }
    }
}
