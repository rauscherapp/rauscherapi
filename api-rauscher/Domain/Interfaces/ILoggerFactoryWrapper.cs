using Microsoft.Extensions.Logging;

namespace Domain.Interfaces
{
    public interface ILoggerFactoryWrapper
    {
        ILoggerFactory LoggerFactory { get; }

        ILogger<T> CreateLogger<T>();
    }
}
