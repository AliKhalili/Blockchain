using Microsoft.AspNetCore.Hosting.Server;
using SHPA.Blockchain.FakeServer.Core.Internal.Connections;
using SHPA.Blockchain.FakeServer.Core.Internal.Middleware;

namespace SHPA.Blockchain.FakeServer.Core.Internal
{
    internal static class HttpConnectionBuilderExtensions
    {
        public static IConnectionBuilder UseHttpServer<TContext>(this IConnectionBuilder builder, ServiceContext serviceContext, IHttpApplication<TContext> application) where TContext : notnull
        {
            var middleware = new HttpConnectionMiddleware<TContext>(serviceContext, application);
            return builder.Use(_ => middleware.OnConnectionAsync);
        }
    }
}