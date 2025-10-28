namespace AgriConnectMarket.SharedKernel.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
