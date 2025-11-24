using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class SeasonRepository(AppDbContext _context) : Repository<Season>(_context), ISeasonRepository
    {
        public async Task<IEnumerable<Season>> GetByFarmIdAsync(Guid farmId, CancellationToken ct = default)
        {
            return await _context.Seasons
                .Where(s => s.FarmId == farmId)
                .ToListAsync(ct);
        }
    }
}
