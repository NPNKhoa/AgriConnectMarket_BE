using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ProfileRepository(AppDbContext _context) : Repository<Profile>(_context), IProfileRepository
    {
        public async Task<Profile?> GetByEmailAsync(string email, bool includeAccount = false)
        {
            var query = _dbContext.Set<Profile>().Where(u => u.Email.Equals(email.Trim()));

            if (includeAccount)
            {
                query = query.Include(u => u.Account);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Profile?> GetByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.Set<Profile>().FirstOrDefaultAsync(u => u.AccountId == accountId);
        }
    }
}
