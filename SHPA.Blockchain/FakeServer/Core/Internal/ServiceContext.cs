using System.IO.Pipelines;
using SHPA.Blockchain.FakeServer.Core.Internal.Infrastructure;

namespace SHPA.Blockchain.FakeServer.Core.Internal
{
    public class ServiceContext
    {
        public IFakeTrace Log { get; set; } = default!;
        public PipeScheduler Scheduler { get; set; } = default!;
        //public IHttpParser<Http1ParsingHandler> HttpParser { get; set; } = default!;
        //public ISystemClock SystemClock { get; set; } = default!;
        //public DateHeaderValueManager DateHeaderValueManager { get; set; } = default!;
        //public ConnectionManager ConnectionManager { get; set; } = default!;
        //public Heartbeat Heartbeat { get; set; } = default!;
        public FakeServerOptions ServerOptions { get; set; } = default!;
    }
}