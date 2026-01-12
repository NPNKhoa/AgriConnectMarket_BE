using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class ViolationRepository : Repository<ViolationReport>, IViolationReportRepository
    {
        public ViolationRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<IReadOnlyList<ViolationReport>> ListAllAsync(bool include = true, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<ViolationReport>().AsNoTracking().Include(r => r.Customer).Include(r => r.Farm).ToListAsync(cancellationToken);
        }
    }
}
