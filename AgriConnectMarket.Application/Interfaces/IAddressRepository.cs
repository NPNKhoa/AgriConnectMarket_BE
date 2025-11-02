using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        public Task<Address> GetAddressByProfileIdAsync(Guid profileId, bool includeProfile = false);
        public Task<IEnumerable<Address>> GetAddressesByProfileIdAsync(Guid profileId, bool includeProfile = false);
    }
}
