using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext _context) : base(_context)
        {

        }

        public async Task<IEnumerable<Profile>> ListAllAsync(bool includeAccount = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Profile>().AsNoTracking();

            if (includeAccount)
            {
                query = query.Include(u => u.Account);
            }

            return await query.ToListAsync();
        }

        public async Task<Profile?> GetByEmailAsync(string email, bool includeAccount = false)
        {
            var query = _dbContext.Set<Profile>().Where(u => u.Email.Equals(email.Trim()));

            if (includeAccount)
            {
                query = query.Include(u => u.Account);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Profile?> GetByAccountIdAsync(Guid accountId, bool includeAccount = false)
        {
            var query = _dbContext.Set<Profile>().Where(u => u.AccountId == accountId);

            if (includeAccount)
            {
                query = query.Include(u => u.Account);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Profile?> GetByIdAsync(Guid profileId, bool includeCart = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Profile>().Where(u => u.Id == profileId);

            if (includeCart)
            {
                query = query.Include(u => u.Cart);
            }

            return await query.FirstOrDefaultAsync(ct);
        }

    }
}
