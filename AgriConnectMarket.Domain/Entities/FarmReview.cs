using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriConnectMarket.Domain.Entities
{
    public class FarmReview : BaseEntity<Guid>, IAuditableEntity
    {
        public Guid CustomerId { get; set; }
        public Guid FarmId { get; set; }
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Account Customer { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual Product Product { get; set; }
        public ReplyReview ReplyReview { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public string? CreatedBy { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public string? UpdatedBy { get ; set ; }

        public FarmReview()
        {
            
        }

        private FarmReview(Guid customerId, Guid farmId, Guid productId, int rating, string content, string? imageUrl)
        {
            CustomerId = customerId;
            FarmId = farmId;
            ProductId = productId;
            Rating = rating;
            Content = content;
            ImageUrl = imageUrl;
        }
    }
}
