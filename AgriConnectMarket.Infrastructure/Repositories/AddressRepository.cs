using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class AddressRepository(AppDbContext _dbContext) : Repository<Address>(_dbContext), IAddressRepository
    {
        public async Task<Address?> GetAddressByProfileIdAsync(Guid profileId, bool includeProfile = false)
        {
            var query = _dbContext.Set<Address>().Where(a => a.ProfileId == profileId);

            if (includeProfile)
            {
                query = query.Include(a => a.Profile);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>?> GetAddressesByProfileIdAsync(Guid profileId, bool includeProfile = false)
        {
            var query = _dbContext.Set<Address>().Where(a => a.ProfileId == profileId);

            if (includeProfile)
            {
                query = query.Include(a => a.Profile);
            }

            return await query.ToListAsync();
        }
    }
}
