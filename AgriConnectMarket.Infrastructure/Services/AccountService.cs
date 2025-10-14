using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.SharedKernel.Interfaces;
using System.Linq.Expressions;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AccountService(AppDbContext _context) : IAccount
    {
        public async Task CreateAsync(Account entity)
        {
            try
            {
                await _context.Accounts.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetAllAsync(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task GetByAsync(Expression<Func<Account, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Account entity)
        {
            throw new NotImplementedException();
        }

        Task IAccount.CreateAsync(Account entity)
        {
            return CreateAsync(entity);
        }

        Task<Task> IGenericInterface<Account>.CreateAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        Task IAccount.DeleteAsync(Guid id)
        {
            return DeleteAsync(id);
        }

        Task<Task> IGenericInterface<Account>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IAccount.GetAllAsync(int pageIndex, int pageSize)
        {
            return GetAllAsync(pageIndex, pageSize);
        }

        Task<Task> IGenericInterface<Account>.GetAllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task IAccount.GetByAsync(Expression<Func<Account, bool>> predicate)
        {
            return GetByAsync(predicate);
        }

        Task<Task> IGenericInterface<Account>.GetByAsync(Expression<Func<Account, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IAccount.GetByIdAsync(Guid id)
        {
            return GetByIdAsync(id);
        }

        Task<Task> IGenericInterface<Account>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IAccount.UpdateAsync(Guid id, Account entity)
        {
            return UpdateAsync(id, entity);
        }

        Task<Task> IGenericInterface<Account>.UpdateAsync(Guid id, Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
