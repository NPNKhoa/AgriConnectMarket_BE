using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CareEventRepository(AppDbContext _dbContext) : Repository<CareEvent>(_dbContext), ICareEventRepository
    {
        public async Task<CareEvent> GetLastByBatchIdAsync(Guid batchId, CancellationToken ct = default)
        {
            return await _dbContext.Set<CareEvent>().Where(c => c.BatchId == batchId).LastOrDefaultAsync();
        }

        public async Task<IReadOnlyList<CareEvent>> GetAllByBatchAsync(Guid batchId, CancellationToken ct)
        {
            return await _dbContext.Set<CareEvent>().Where(c => c.BatchId == batchId).ToListAsync();
        }

        public Task<IReadOnlyList<CareEvent>> GetPreviousEventAsync(Guid batchId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
