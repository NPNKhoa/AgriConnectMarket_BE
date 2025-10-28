using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        public Task<Profile?> GetByEmailAsync(string email);
    }
}
