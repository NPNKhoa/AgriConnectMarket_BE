using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class FarmRepository(AppDbContext _context) : Repository<Farm>(_context), IFarmRepository
    {
    }
}
