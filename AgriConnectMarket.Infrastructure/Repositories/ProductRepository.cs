using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext _dbContext) : Repository<Product>(_dbContext), IProductRepository
    {
        public async Task<Product?> GetByIdAsync(Guid productId, bool includeCategory = false, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<Product>().Where(p => p.Id == productId);

            if (includeCategory)
            {
                query = query.Include(p => p.Category);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByFarmIdAsync(Guid farmId, bool includeCategory = false, bool includeSeason = false, bool includePreOrder = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Product>()
                    .Where(p => p.Seasons.Any(s => s.FarmId == farmId));

            if (includeCategory)
            {
                query = query.Include(p => p.Category);
            }

            if (includeSeason)
            {
                query = query.Include(p => p.Seasons);
            }

            if (includePreOrder)
            {
                query = query.Include(p => p.PreOrder);
            }

            return await query.ToListAsync();
        }
    }
}
