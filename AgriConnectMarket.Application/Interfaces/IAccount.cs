using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;
using System.Linq.Expressions;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IAccount : IGenericInterface<Account>
    {
        Task CreateAsync(Account entity);
        Task UpdateAsync(Guid id, Account entity);
        Task DeleteAsync(Guid id);
        Task GetAllAsync(int pageIndex = 1, int pageSize = 10);
        Task GetByIdAsync(Guid id);
        Task GetByAsync(Expression<Func<Account, bool>> predicate);
    }
}
