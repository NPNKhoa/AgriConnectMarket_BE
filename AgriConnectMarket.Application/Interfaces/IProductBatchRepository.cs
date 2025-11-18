using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProductBatchRepository : IRepository<ProductBatch>
    {
        public Task<IEnumerable<ProductBatch>> GetBySeasonAsync(Guid seasonId, bool includeSeason = false, CancellationToken ct = default);
    }
}
