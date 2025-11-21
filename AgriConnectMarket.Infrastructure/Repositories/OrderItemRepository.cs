using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class OrderItemRepository(AppDbContext _dbContext) : Repository<OrderItem>(_dbContext), IOrderItemRepository
    {
    }
}
