using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IAuthenRepository : IRepository<Account>
    {
        public Task<Account> GetByUsernameAsync(string username);
        public Task<Account> GetByUsernameAsync(string username, bool includeProfile = false);
        public Task<string> GetRolesAsync(Guid accountId);
    }
}
