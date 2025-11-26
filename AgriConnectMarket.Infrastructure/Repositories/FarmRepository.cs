using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class FarmRepository(AppDbContext _dbContext) : Repository<Farm>(_dbContext), IFarmRepository
    {
        public async Task<Farm> GetFarmByAccount(Guid accountId, bool includeAddress = false, bool includeFarmer = false, bool includeSeason = false)
        {
            var query = _dbContext.Set<Farm>().Where(f => f.FarmerId == accountId);

            if (includeAddress)
            {
                query = query.Include(f => f.Address);
            }

            if (includeFarmer)
            {
                query = query.Include(f => f.Farmer);
            }

            if (includeSeason)
            {
                query = query.Include(f => f.Seasons);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Farm> GetByIdAsync(Guid farmId, bool includeAddress = false, bool includeFarmer = false, bool includeSeason = false)
        {
            var query = _dbContext.Set<Farm>().Where(f => f.Id == farmId && !f.IsDelete);

            if (includeAddress)
            {
                query = query.Include(f => f.Address);
            }

            if (includeFarmer)
            {
                query = query.Include(f => f.Farmer);
            }

            if (includeSeason)
            {
                query = query.Include(f => f.Seasons);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
