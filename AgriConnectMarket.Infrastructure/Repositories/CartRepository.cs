using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<Cart> GetByProfileIdAsync(Guid profileId, bool includeItems = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Cart>().Where(c => c.CustomerId == profileId);

            if (includeItems)
            {
                query = query.Include(c => c.CartItems)!
                                .ThenInclude(i => i.Batch)
                                    .ThenInclude(b => b.Season)
                                        .ThenInclude(s => s.Farm)
                                            .ThenInclude(f => f.Address)
                                    .Include(c => c.CartItems)!
                                        .ThenInclude(i => i.Batch)
                                            .ThenInclude(b => b.Season)
                                                .ThenInclude(s => s.Product)
                                                    .ThenInclude(p => p.Category);
            }

            if (includeProfile)
            {
                query = query.Include(c => c.Customer);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cart> GetByIdAsync(Guid cartId, bool includeItems = false, bool includeProfile = false, bool withoutTracking = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Cart>().Where(c => c.Id == cartId);

            if (withoutTracking)
            {
                query = query.AsNoTracking();
            }

            if (includeItems)
            {
                query = query.Include(c => c.CartItems)
                                .ThenInclude(i => i.Batch)
                                    .ThenInclude(b => b.Season)
                                        .ThenInclude(s => s.Farm)
                                            .ThenInclude(f => f.Address);
            }

            if (includeProfile)
            {
                query = query.Include(c => c.Customer);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cart> GetByItemIdAsync(Guid itemId, bool includeItems = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Cart>().Where(c => c.CartItems.Any(i => i.Id == itemId)).AsNoTracking();

            if (includeItems)
            {
                query = query.Include(c => c.CartItems);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
