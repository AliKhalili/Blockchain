using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;

namespace SHPA.Blockchain.FakeServer.Core.Internal
{
    public class HttpConnection
    {
        private readonly HttpConnectionContext _context;

        public HttpConnection(HttpConnectionContext context)
        {
            _context = context;
        }

        public async Task ProcessRequestsAsync<TContext>(IHttpApplication<TContext> httpApplication) where TContext : notnull
        {
            throw new System.NotImplementedException();
        }
    }
}