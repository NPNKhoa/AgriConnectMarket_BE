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

        public async Task<Result<Transaction>> GetByIdAsync(Guid transactionId, CancellationToken ct = default)
        {
            var tx = await _uow.TransactionRepository.GetByIdAsync(transactionId, ct);

            if (tx is null)
            {
                return Result<Transaction>.Fail(MessageConstant.TRANSACTION_NOT_FOUND);
            }

            return Result<Transaction>.Success(tx);
        }

        public async Task<Result> ResolveTransactionAsync(Guid txId, CancellationToken ct = default)
        {
            var tx = await _uow.TransactionRepository.GetByIdAsync(txId, ct);

            if (tx is null)
            {
                return Result.Fail(MessageConstant.TRANSACTION_NOT_FOUND);
            }

            tx.ResolveTransaction();

            await _uow.TransactionRepository.UpdateAsync(tx, ct);
            await _uow.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
