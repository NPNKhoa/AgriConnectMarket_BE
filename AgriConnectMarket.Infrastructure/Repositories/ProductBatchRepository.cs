using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ProductBatchRepository(AppDbContext _dbContext) : Repository<ProductBatch>(_dbContext), IProductBatchRepository
    {
        public async Task<IEnumerable<ProductBatch>> GetBySeasonAsync(Guid seasonId, bool includeSeason = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<ProductBatch>().Where(pb => pb.SeasonId.Equals(seasonId));

            if (includeSeason)
            {
                query = query.Include(pb => pb.Season);
            }

            return await query.ToListAsync(ct);
        }

        public async Task<IEnumerable<ProductBatch>> GetByFarmAsync(Guid farmId, bool includeSeason = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<ProductBatch>().Where(pb => pb.Season.FarmId.Equals(farmId));

            if (includeSeason)
            {
                query = query.Include(pb => pb.Season);
            }

            return await query.ToListAsync(ct);
        }
    }
}
