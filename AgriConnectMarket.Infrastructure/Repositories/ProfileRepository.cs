using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ProfileRepository(AppDbContext _context) : Repository<Profile>(_context), IProfileRepository
    {
        public async Task<Profile?> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<Profile>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
