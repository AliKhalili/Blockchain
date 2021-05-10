using Microsoft.Extensions.Logging;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Infrastructure
{
    public interface IFakeTrace : ILogger
    {
        void ConnectionAccepted(string connectionId);
    }
}