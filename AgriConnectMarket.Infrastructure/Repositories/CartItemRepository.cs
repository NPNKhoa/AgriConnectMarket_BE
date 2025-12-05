using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<CartItem> GetByCartAndBatchAsync(Guid cartId, Guid batchId, CancellationToken ct = default)
        {
            return await _dbContext.Set<CartItem>().Where(ci => ci.CartId == cartId && ci.BatchId == batchId).FirstOrDefaultAsync();
        }
    }
}
