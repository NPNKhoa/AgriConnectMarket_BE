using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class PreOrderRepository : Repository<PreOrder>, IPreOrderRepository
    {
        public PreOrderRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<PreOrder> GetByIdAsync(Guid id, bool include = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<PreOrder>().Where(p => p.OrderId == id);


            if (include)
            {
                query = query.Include(p => p.Order).ThenInclude(o => o.Customer);
            }

            return await query.FirstOrDefaultAsync(ct);
        }
    }
}
