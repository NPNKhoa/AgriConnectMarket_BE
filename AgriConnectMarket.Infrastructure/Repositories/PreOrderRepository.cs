using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class PreOrderRepository : Repository<PreOrder>, IPreOrderRepository
    {
        public PreOrderRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
