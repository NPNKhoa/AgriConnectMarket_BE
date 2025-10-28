using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class AuthenRepository(AppDbContext _dbContext) : Repository<Account>(_dbContext), IAuthenRepository
    {
        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Set<Account>().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Account?> GetByUsernameAsync(string username, bool includeProfile = false)
        {
            var query = _dbContext.Set<Account>().Where(u => u.UserName == username);

            if (includeProfile)
            {
                query.Include(u => u.Profile);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
