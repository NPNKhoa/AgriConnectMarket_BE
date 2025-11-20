using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CartRepository(AppDbContext _dbContext) : Repository<Cart>(_dbContext), ICartRepository
    {
        public async Task<Cart> GetByProfileIdAsync(Guid profileId, bool includeItems = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Cart>().Where(c => c.CustomerId == profileId);

            if (includeItems)
            {
                query = query.Include(c => c.CartItems);
            }

            if (includeProfile)
            {
                query = query.Include(c => c.Customer);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cart> GetByIdAsync(Guid cartId, bool includeItems = false, bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Cart>().Where(c => c.Id == cartId);

            if (includeItems)
            {
                query = query.Include(c => c.CartItems);
            }

            if (includeProfile)
            {
                query = query.Include(c => c.Customer);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
