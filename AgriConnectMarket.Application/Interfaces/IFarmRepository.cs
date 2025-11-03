using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IFarmRepository : IRepository<Farm>
    {
        public Task<Farm> GetFarmByAccount(Guid accountId, bool includeAddress = false, bool includeFarmer = false, bool includeSeason = false);
        public Task<Farm> GetByIdAsync(Guid farmId, bool includeAddress = false, bool includeFarmer = false, bool includeSeason = false);
    }
}
