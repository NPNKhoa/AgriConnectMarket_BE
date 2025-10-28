namespace AgriConnectMarket.SharedKernel.Loggers
{
    public interface ILoggerAdapter<T>
    {
        public void LogInfo(string message, params object[] args);
        public void LogError(Exception ex, string message, params object[] args);
    }
}
