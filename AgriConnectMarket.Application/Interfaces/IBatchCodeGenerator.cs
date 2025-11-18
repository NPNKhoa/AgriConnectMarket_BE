using AgriConnectMarket.Domain.ValueObjects;

namespace AgriConnectMarket.Application.Interfaces
{
    public interface IBatchCodeGenerator
    {
        Task<BatchCode> GenerateNextCodeAsync(string prefix, CancellationToken ct = default);
    }
}
