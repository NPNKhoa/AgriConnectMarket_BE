using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        public Task<Address> GetAddressByProfileIdAsync(Guid profileId, bool includeProfile = false);
        public Task<Address> GetDefaultAddressAsync(bool includeProfile = false, CancellationToken ct = default);
        public Task<IEnumerable<Address>> GetAddressesByProfileIdAsync(Guid profileId, bool includeProfile = false);
    }
}
