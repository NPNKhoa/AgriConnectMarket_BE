using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class PreOrderRepository(AppDbContext _dbContext) : Repository<PreOrder>(_dbContext), IPreOrderRepository
    {
    }
}
