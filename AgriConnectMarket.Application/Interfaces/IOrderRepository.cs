using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrderByProfileIdAsync(Guid profileId, bool includeItems = false, bool includePreOrder = false, bool includeProfile = false, CancellationToken ct = default);
        public Task<IEnumerable<Order>> GetPreOrderByProfileIdAsync(Guid profileId, bool includeItems = false, bool includePreOrder = false, bool includeProfile = false, CancellationToken ct = default);
        public Task<IEnumerable<Order>> GetOrdersByFarmIdAsync(Guid farmId, bool includeItems = false, bool includepPreOrder = false, bool includeProfile = false, CancellationToken ct = default);
        public Task<Order> GetByIdAsync(Guid orderId, bool includeItems = false, bool includepPreOrder = false, bool includeProfile = false, CancellationToken ct = default);
    }
}
