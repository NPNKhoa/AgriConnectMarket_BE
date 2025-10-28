using AgriConnectMarket.SharedKernel.Loggers;
using Microsoft.Extensions.Logging;

namespace AgriConnectMarket.Infrastructure.Logging
{
    public class LoggerAdapter<T>(ILogger<T> _logger) : ILoggerAdapter<T>
    {
        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
