using System;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.FakeServer.Core;
using SHPA.Blockchain.FakeServer.Core.Internal;
using SHPA.Blockchain.FakeServer.Core.Internal.Infrastructure;

namespace SHPA.Blockchain.FakeServer
{
    public class FakeServerImpl : IServer
    {

        private readonly IConnectionListenerFactory _transportFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="FakeServer"/>.
        /// </summary>
        /// <param name="options">The Kestrel <see cref="IOptions{TOptions}"/>.</param>
        /// <param name="transportFactory">The <see cref="IConnectionListenerFactory"/>.</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
        public FakeServerImpl(IOptions<FakeServerOptions> options, IConnectionListenerFactory transportFactory, ILoggerFactory loggerFactory)
        {
            _transportFactory = transportFactory;
            ServiceContext = CreateServiceContext(options, loggerFactory);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) where TContext : notnull
        {
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public IFeatureCollection Features { get; }
        private ServiceContext ServiceContext { get; }


        private static ServiceContext CreateServiceContext(IOptions<FakeServerOptions> options,
            ILoggerFactory loggerFactory)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            var serverOptions = options.Value ?? new FakeServerOptions();
            var trace = new FakeTrace(loggerFactory);
            return new ServiceContext
            {
                Log = trace,
                Scheduler = PipeScheduler.ThreadPool,
                //HttpParser = new HttpParser<Http1ParsingHandler>(trace.IsEnabled(LogLevel.Information)),
                //SystemClock = heartbeatManager,
                //DateHeaderValueManager = dateHeaderValueManager,
                //ConnectionManager = connectionManager,
                //Heartbeat = heartbeat,
                ServerOptions = serverOptions
            };
        }
    }
}