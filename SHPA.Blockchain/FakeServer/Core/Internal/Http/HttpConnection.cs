using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Http
{
    public class HttpConnection
    {
        private readonly HttpConnectionContext _context;

        public HttpConnection(HttpConnectionContext context)
        {
            _context = context;
        }

        public async Task ProcessRequestsAsync<TContext>(IHttpApplication<TContext> application) where TContext : notnull
        {
            var context = application.CreateContext(_context.ConnectionFeatures);
            try
            {
                //todo log request start
                await application.ProcessRequestAsync(context);
                //todo log request stop
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}