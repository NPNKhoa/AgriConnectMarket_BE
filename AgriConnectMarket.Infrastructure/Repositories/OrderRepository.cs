using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext _dbContext) : Repository<Order>(_dbContext), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrderByProfileIdAsync(Guid profileId, bool includeItems = false, bool includePreOrder = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Order>().Where(o => o.CustomerId == profileId);

            if (includeItems)
            {
                query = query.Include(o => o.OrderItems);
            }

            if (includePreOrder)
            {
                query = query.Include(o => o.PreOrder);
            }

            if (includeProfile)
            {
                query = query.Include(o => o.Customer);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByFarmIdAsync(Guid farmId, bool includeItems = false, bool includepPreOrder = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext
                .Set<Order>()
                .Where(o => o.OrderItems.Any(i => i.Batch.Season.FarmId == farmId));

            if (includeItems)
            {
                query = query.Include(o => o.OrderItems);
            }

            if (includepPreOrder)
            {
                query = query.Include(o => o.PreOrder);
            }

            if (includeProfile)
            {
                query = query.Include(o => o.Customer);
            }

            return await query.ToListAsync(ct);
        }


        public async Task<Order> GetByIdAsync(Guid orderId, bool includeItems = false, bool includepPreOrder = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext
                .Set<Order>()
                .Where(o => o.Id == orderId);

            if (includeItems)
            {
                query = query.Include(o => o.OrderItems);
            }

            if (includepPreOrder)
            {
                query = query.Include(o => o.PreOrder);
            }

            if (includeProfile)
            {
                query = query.Include(o => o.Customer);
            }

            return await query.FirstOrDefaultAsync(ct);
        }
    }
}
