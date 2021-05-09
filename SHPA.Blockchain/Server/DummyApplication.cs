using System;
using System.Threading.Tasks;
using SHPA.Blockchain.Server.Http;
using HttpContext = SHPA.Blockchain.Server.Http.HttpContext;
using RequestDelegate = SHPA.Blockchain.Server.Http.RequestDelegate;

namespace SHPA.Blockchain.Server
{
    public class DummyApplication : IHttpApplication<HttpContext>
    {
        private readonly RequestDelegate _requestDelegate;

        public DummyApplication()
            : this(_ => Task.CompletedTask)
        {
        }

        public DummyApplication(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public HttpContext CreateContext(IFeatureCollection contextFeatures)
        {
            return new DefaultHttpContext();
            //return _httpContextFactory?.Create(contextFeatures) ?? new DefaultHttpContext(contextFeatures);
        }

        public void DisposeContext(HttpContext context, Exception exception)
        {
            //_httpContextFactory?.Dispose(context);
        }

        public async Task ProcessRequestAsync(HttpContext context)
        {
            await _requestDelegate(context);
        }
    }
}