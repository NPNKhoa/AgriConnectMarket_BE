using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProductBatchRepository : IRepository<ProductBatch>
    {
        public Task<IEnumerable<ProductBatch>> ListAllAsync(bool includeImages = false, CancellationToken ct = default);
        public Task<ProductBatch> GetByIdAsync(Guid batchId, bool includeSeason = false, bool includeReview = false, bool withoutTracking = false, CancellationToken ct = default);
        public Task<ProductBatch> GetByIdAsync(Guid batchId, bool includeAllRelated = false, bool withoutTracking = false, CancellationToken ct = default);
        public Task<IEnumerable<ProductBatch>> GetBySeasonAsync(Guid seasonId, bool includeSeason = false, CancellationToken ct = default);
        public Task<IEnumerable<ProductBatch>> GetByFarmAsync(Guid farmId, bool includeSeason = false, CancellationToken ct = default);
        public Task<IEnumerable<ProductBatch>> GetByFarmerAsync(Guid farmerId, bool includeAllRelated = false, CancellationToken ct = default);
        Task<IReadOnlyList<ProductBatch>> ListAsync(ISpecification<ProductBatch> spec, bool includeRelated = false, CancellationToken cancellationToken = default);
    }
}
