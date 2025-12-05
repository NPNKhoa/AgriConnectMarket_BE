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
    }
}
