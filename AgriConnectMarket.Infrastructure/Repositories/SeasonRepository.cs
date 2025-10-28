using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class SeasonRepository(AppDbContext _context) : Repository<Season>(_context), ISeasonRepository
    {

    }
}
