using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IPreOrderRepository : IRepository<PreOrder>
    {
        public Task<PreOrder> GetByIdAsync(Guid id, bool include, CancellationToken ct = default);
    }
}
