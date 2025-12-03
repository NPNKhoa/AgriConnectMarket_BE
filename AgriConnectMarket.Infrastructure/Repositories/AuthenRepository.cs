using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class AuthenRepository : Repository<Account>, IAuthenRepository
    {
        public AuthenRepository(AppDbContext _dbContext) : base(_dbContext) { }

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

        public async Task<string> GetRolesAsync(Guid accountId)
        {
            var account = await _dbContext.Set<Account>().FirstOrDefaultAsync(a => a.Id == accountId);

            return account?.Role ?? "";
        }
    }
}
