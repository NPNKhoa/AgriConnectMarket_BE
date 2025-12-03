using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CareEventRepository : Repository<CareEvent>, ICareEventRepository
    {
        public CareEventRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<CareEvent> GetLastByBatchIdAsync(Guid batchId, CancellationToken ct = default)
        {
            return await _dbContext.Set<CareEvent>().Where(c => c.BatchId == batchId).OrderByDescending(c => c.OccurredAt).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<CareEvent>> GetAllByBatchAsync(Guid batchId, CancellationToken ct)
        {
            return await _dbContext.Set<CareEvent>().Where(c => c.BatchId == batchId).Include(c => c.EventType).OrderBy(e => e.OccurredAt).ToListAsync();
        }

        public Task<IReadOnlyList<CareEvent>> GetPreviousEventAsync(Guid batchId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
