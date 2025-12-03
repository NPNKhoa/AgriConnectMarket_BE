using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Task<Transaction> GetTransactionByRef(string txRef, bool includeOrder = false, CancellationToken ct = default);
    }
}
