using Microsoft.AspNetCore.Hosting.Server.Abstractions;
using Microsoft.AspNetCore.Http.Features;

namespace SHPA.Blockchain.SimpleServer
{
    public class HttpConnection<TContext> : IHostContextContainer<TContext> where TContext : notnull, IFeatureCollection
    {
        public TContext HostContext { get; set; }
    }
}