using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProductBatchRepository : IRepository<ProductBatch>
    {
        public Task<IEnumerable<ProductBatch>> GetBySeasonAsync(Guid seasonId, bool includeSeason = false, CancellationToken ct = default);
        public Task<IEnumerable<ProductBatch>> GetByFarmAsync(Guid farmId, bool includeSeason = false, CancellationToken ct = default);
    }
}
