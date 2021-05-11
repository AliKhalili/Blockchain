using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using SHPA.Blockchain.FakeServer.Core.Internal.Connections;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Middleware
{
    internal class HttpConnectionMiddleware<TContext> where TContext : notnull
    {
        private readonly ServiceContext _serviceContext;
        private readonly IHttpApplication<TContext> _application;

        public HttpConnectionMiddleware(ServiceContext serviceContext, IHttpApplication<TContext> application)
        {
            _serviceContext = serviceContext;
            _application = application;
        }

        public Task OnConnectionAsync(ConnectionContext connectionContext)
        {
            return new HttpConnection(new HttpConnectionContext()).ProcessRequestsAsync(_application);
        }
    }
}