using Microsoft.AspNetCore.Http.Features;
using SHPA.Blockchain.FakeServer.Core.Internal.Connections;

namespace SHPA.Blockchain.FakeServer.Core.Internal
{
    public class HttpConnectionContext
    {
        public string ConnectionId { get; }
        public ConnectionContext ConnectionContext { get; }
        public ServiceContext ServiceContext { get; }
        public IFeatureCollection ConnectionFeatures { get; }

    }
}