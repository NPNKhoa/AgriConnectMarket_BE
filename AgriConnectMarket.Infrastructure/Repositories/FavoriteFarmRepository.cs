using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class FavoriteFarmRepository : Repository<FavoriteFarm>, IFavoriteFarmRepository
    {
        public FavoriteFarmRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<IEnumerable<FavoriteFarm>> GetByProfileAsync(Guid profileId, bool includeProfile = false, bool includeFarm = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<FavoriteFarm>().Where(f => f.CustomerId == profileId);

            if (includeFarm)
            {
                query = query.Include(f => f.Farm);
            }

            if (includeProfile)
            {
                query = query.Include(f => f.Customer);
            }

            return await query.ToListAsync();
        }

        public async Task<FavoriteFarm> GetByFKsAsync(Guid profileId, Guid farmId, bool includeProfile = false, bool includeFarm = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<FavoriteFarm>().Where(f => f.CustomerId == profileId && f.FarmId == farmId);

            if (includeFarm)
            {
                query = query.Include(f => f.Farm);
            }

            if (includeProfile)
            {
                query = query.Include(f => f.Customer);
            }

            return await query.FirstOrDefaultAsync()!;
        }
    }
}
