using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface ICareEventRepository : IRepository<CareEvent>
    {
        public Task<CareEvent> GetLastByBatchIdAsync(Guid batchId, CancellationToken ct);
        public Task<IReadOnlyList<CareEvent>> GetAllByBatchAsync(Guid batchId, CancellationToken ct);
        public Task<IReadOnlyList<CareEvent>> GetPreviousEventAsync(Guid batchId, CancellationToken ct);
    }
}
