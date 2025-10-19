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
    }
}
