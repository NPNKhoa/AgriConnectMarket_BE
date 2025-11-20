using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Task<Cart> GetByProfileIdAsync(Guid profileId, bool includeItems = false, bool includeProfile = false, CancellationToken ct = default);
        public Task<Cart> GetByIdAsync(Guid cartId, bool includeItems = false, bool includeProfile = false, CancellationToken ct = default);
    }
}
