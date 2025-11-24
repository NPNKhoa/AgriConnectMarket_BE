using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ISeasonRepository : IRepository<Season>
    {
        Task<IEnumerable<Season>> GetByFarmIdAsync(Guid farmId, CancellationToken ct = default);
    }
}
