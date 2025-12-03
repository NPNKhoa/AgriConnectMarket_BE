using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext _dbContext) : Repository<Transaction>(_dbContext), ITransactionRepository
    {
        public async Task<Transaction> GetTransactionByRef(string txRef, bool includeOrder = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Transaction>().AsNoTracking().Where(tx => tx.TransactionRef.Equals(txRef));

            if (includeOrder)
            {
                query = query.Include(t => t.Order);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
