using AgriConnectMarket.Domain.Entities;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IPasswordOtpRepository : IRepository<PasswordOtp>
    {
        Task<PasswordOtp?> GetLatestForUserAsync(Guid userId, string purpose, CancellationToken ct);
        Task InvalidateAllForUserAsync(Guid userId, string purpose, CancellationToken ct);
    }
}
