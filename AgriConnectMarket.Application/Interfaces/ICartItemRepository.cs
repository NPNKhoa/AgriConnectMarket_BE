using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        public Task<CartItem> GetByCartAndBatchAsync(Guid cartId, Guid batchId, CancellationToken ct = default);
    }
}
