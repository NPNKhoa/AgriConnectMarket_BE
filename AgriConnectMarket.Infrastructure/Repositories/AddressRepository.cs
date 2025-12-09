using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext _dbContext) : base(_dbContext) { }

        public async Task<Address> GetAddressByProfileIdAsync(Guid profileId, bool includeProfile = false)
        {
            var query = _dbContext.Set<Address>().Where(a => a.ProfileId == profileId && !a.IsDelete);

            if (includeProfile)
            {
                query = query.Include(a => a.Profile);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesByProfileIdAsync(Guid profileId, bool includeProfile = false)
        {
            var query = _dbContext.Set<Address>().Where(a => a.ProfileId == profileId && !a.IsDelete);

            if (includeProfile)
            {
                query = query.Include(a => a.Profile);
            }

            return await query.ToListAsync();
        }

        public async Task<Address> GetDefaultAddressAsync(bool includeProfile = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Address>().Where(a => a.IsDefault && !a.IsDelete);

            if (includeProfile)
            {
                query = query.Include(a => a.Profile);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
