using System;
using System.Collections.Concurrent;
using System.IO.Pipelines;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.FakeServer.Core;
using SHPA.Blockchain.FakeServer.Core.Internal;
using SHPA.Blockchain.FakeServer.Core.Internal.Connections;
using SHPA.Blockchain.FakeServer.Core.Internal.Infrastructure;

namespace SHPA.Blockchain.FakeServer
{
    public class FakeServerImpl : IServer
    {
        private readonly HttpListener _listener;
        private readonly IConnectionBuilder _connectionBuilder;
        private readonly SemaphoreSlim _bindSemaphore = new(initialCount: 1);

        /// <summary>
        /// Initializes a new instance of <see cref="FakeServer"/>.
        /// </summary>
        /// <param name="options">The Kestrel <see cref="IOptions{TOptions}"/>.</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
        public FakeServerImpl(IOptions<FakeServerOptions> options, ILoggerFactory loggerFactory)
        {
            ServiceContext = CreateServiceContext(options, loggerFactory);
            Features = new FeatureCollection();
            Features.Set<IServerAddressesFeature>(new ServerAddressesFeature());
            _listener = new HttpListener();
            _connectionBuilder = new DefaultConnectionBuilder(ServiceContext.ServerOptions.ApplicationServices);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public async Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) where TContext : notnull
        {
            _listener.Prefixes.Add("http://localhost:8081/");
            _connectionBuilder.UseHttpServer(ServiceContext, application);
            await BindAsync(application, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public IFeatureCollection Features { get; }
        private ServiceContext ServiceContext { get; }


        private async Task BindAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            await _bindSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                _listener.Start();
                var connectionDelegate = _connectionBuilder.Build();
                while (_listener.IsListening || !cancellationToken.IsCancellationRequested)
                {
                    var asyncResult = _listener.BeginGetContext(ar =>
                    {
                        var context = _listener.EndGetContext(ar);
                        //todo: implement thread pool to process request concurrently
                        connectionDelegate(new ConnectionContext(context));
                    }, application);
                    asyncResult.AsyncWaitHandle.WaitOne();
                }
            }
            finally
            {
                _bindSemaphore.Release();
            }
        }
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