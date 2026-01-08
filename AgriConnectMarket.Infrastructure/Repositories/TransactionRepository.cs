using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }

        public async Task<Transaction> GetTransactionByRef(string txRef, bool includeOrder = false, CancellationToken ct = default)
        {
            var query = _dbContext.Set<Transaction>().AsNoTracking().Where(tx => tx.TransactionRef.Equals(txRef));

            if (includeOrder)
            {
                query = query.Include(tx => tx.Orders);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
