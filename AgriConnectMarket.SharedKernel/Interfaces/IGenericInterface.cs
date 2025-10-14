using System.Linq.Expressions;

namespace AgriConnectMarket.SharedKernel.Interfaces
{
    public interface IGenericInterface<T> where T : class
    {
        Task<Task> CreateAsync(T entity);
        Task<Task> UpdateAsync(Guid id, T entity);
        Task<Task> DeleteAsync(Guid id);
        Task<Task> GetAllAsync(int pageIndex = 1, int pageSize = 10);
        Task<Task> GetByIdAsync(Guid id);
        Task<Task> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
