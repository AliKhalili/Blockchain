using System;
using Microsoft.Extensions.Logging;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Infrastructure
{
    public class FakeTrace : IFakeTrace
    {
        private static readonly Action<ILogger, string, Exception?> _connectionAccepted =
            LoggerMessage.Define<string>(LogLevel.Debug, new EventId(39, "ConnectionAccepted"), @"Connection id ""{ConnectionId}"" accepted.");

        protected readonly ILogger _connectionsLogger;
        protected readonly ILogger _generalLogger;

        public FakeTrace(ILoggerFactory loggerFactory)
        {
            _generalLogger = loggerFactory.CreateLogger("FakeServer");
            _connectionsLogger = loggerFactory.CreateLogger("FakeServer.Connections");
        }

        public IDisposable BeginScope<TState>(TState state)
            => _generalLogger.BeginScope(state);

        public bool IsEnabled(LogLevel logLevel) => _generalLogger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _generalLogger.Log(logLevel, eventId, state, exception, formatter);


        public void ConnectionAccepted(string connectionId)
        {
            _connectionAccepted(_connectionsLogger, connectionId, null);
        }
    }
}