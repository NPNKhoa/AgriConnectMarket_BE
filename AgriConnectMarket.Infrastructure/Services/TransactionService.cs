using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class TransactionService(IUnitOfWork _uow)
    {
        public async Task<Result<IReadOnlyList<Transaction>>> GetAllTransactionsAsync(CancellationToken ct = default)
        {
            var transactions = await _uow.TransactionRepository.ListAllAsync(ct);

            if (!transactions.Any())
            {
                return Result<IReadOnlyList<Transaction>>.Fail(MessageConstant.TRANSACTION_NOT_FOUND);
            }

            return Result<IReadOnlyList<Transaction>>.Success(transactions);
        }
    }
}
