using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class ProductBatchImage : BaseEntity<Guid>
    {
        public string ImageUrl { get; set; }
        public Guid BatchId { get; set; }
        public virtual ProductBatch Batch { get; set; }

        public ProductBatchImage()
        {

        }

        public ProductBatchImage(Guid batchId, string imageUrl)
        {
            BatchId = batchId;
            ImageUrl = imageUrl;
        }

    }
}
