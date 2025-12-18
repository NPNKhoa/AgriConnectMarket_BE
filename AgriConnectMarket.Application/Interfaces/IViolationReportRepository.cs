using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IViolationReportRepository : IRepository<ViolationReport>
    {
        Task<IReadOnlyList<ViolationReport>> ListAllAsync(bool include, CancellationToken cancellationToken = default);
    }
}
