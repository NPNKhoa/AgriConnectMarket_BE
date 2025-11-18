using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IFavoriteFarmRepository : IRepository<FavoriteFarm>
    {
        public Task<IEnumerable<FavoriteFarm>> GetByProfileAsync(Guid profileId, bool includeProfile = false, bool includeFarm = false, CancellationToken ct = default);
        public Task<FavoriteFarm> GetByFKsAsync(Guid profileId, Guid farmId, bool includeProfile = false, bool includeFarm = false, CancellationToken ct = default);
    }
}
