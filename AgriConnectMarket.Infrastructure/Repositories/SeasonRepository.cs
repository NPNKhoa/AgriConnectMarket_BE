using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonRepository(AppDbContext _context) : base(_context)
        {

        }

        public async Task<IEnumerable<Season>> GetByFarmIdAsync(Guid farmId, CancellationToken ct = default)
        {
            return await _dbContext.Set<Season>()
                .Where(s => s.FarmId == farmId)
                .ToListAsync(ct);
        }
    }
}
