using AgriConnectMarket.SharedKernel.Interfaces;

namespace AgriConnectMarket.Infrastructure.Services
{
    public sealed class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
