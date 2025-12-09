using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class PasswordOtpRepository : Repository<PasswordOtp>, IPasswordOtpRepository
    {
        public PasswordOtpRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<PasswordOtp?> GetLatestForUserAsync(Guid userId, string purpose, CancellationToken ct)
        {
            return await _dbContext.Set<PasswordOtp>()
                .Where(x => x.UserId == userId && x.Purpose == purpose && !x.Consumed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(ct);
        }

        public async Task InvalidateAllForUserAsync(Guid userId, string purpose, CancellationToken ct)
        {
            var list = await _dbContext.Set<PasswordOtp>().Where(x => x.UserId == userId && x.Purpose == purpose && !x.Consumed).ToListAsync(ct);
            foreach (var l in list) l.RegisterAttempt(true);
            _dbContext.Set<PasswordOtp>().UpdateRange(list);
            await _dbContext.SaveChangesAsync(ct);
        }
    }

}
