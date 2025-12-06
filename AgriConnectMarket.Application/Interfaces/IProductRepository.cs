using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> ListAllAsync(bool includeCategory = false, CancellationToken ct = default);
        public Task<Product?> GetByIdAsync(Guid productId, bool includeCategory = false, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Product>> GetProductsByFarmIdAsync(Guid farmId, bool includeCategory = false, bool includeSeason = false, CancellationToken ct = default);
    }
}
