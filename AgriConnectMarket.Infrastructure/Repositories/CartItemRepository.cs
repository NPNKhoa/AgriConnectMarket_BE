using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CartItemRepository(AppDbContext _dbContext) : Repository<CartItem>(_dbContext), ICartItemRepository
    {
        public async Task<CartItem> GetByCartAndBatchAsync(Guid cartId, Guid batchId, CancellationToken ct = default)
        {
            return await _dbContext.Set<CartItem>().Where(ci => ci.CartId == cartId && ci.BatchId == batchId).FirstOrDefaultAsync();
        }
    }
}
