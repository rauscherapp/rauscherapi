using Microsoft.Extensions.Logging;
using Domain.Interfaces;

namespace Domain.Services
{
    public class LoggerFactoryWrapper : ILoggerFactoryWrapper
    {
        public ILoggerFactory LoggerFactory { get; }

        public LoggerFactoryWrapper(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public ILogger<T> CreateLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }
    }
}
