using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        public Task<IEnumerable<OrderItem>> ListAsync(ISpecification<OrderItem> spec, bool includeProduct = false, CancellationToken ct = default);
    }
}
