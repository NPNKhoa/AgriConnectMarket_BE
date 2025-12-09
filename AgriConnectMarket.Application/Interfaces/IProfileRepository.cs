using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        public Task<IEnumerable<Profile>> ListAllAsync(bool includeAccount = false, CancellationToken ct = default);
        public Task<Profile?> GetByEmailAsync(string email, bool includeAccount = false);
        public Task<Profile?> GetByAccountIdAsync(Guid accountId, bool includeAccount = false);
        public Task<Profile?> GetByIdAsync(Guid profileId, bool includeCart = false, CancellationToken ct = default);
    }
}
