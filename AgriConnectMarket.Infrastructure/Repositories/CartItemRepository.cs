using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CartItemRepository(AppDbContext _dbContext) : Repository<CartItem>(_dbContext), ICartItemRepository
    {
    }
}
