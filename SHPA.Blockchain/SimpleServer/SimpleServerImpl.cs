using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.SimpleServer
{
    public class SimpleServerImpl : IServer
    {
        private readonly HttpListener _listener;
        private readonly SimpleServerOptions _options;
        private readonly ILogger _logger;

        public SimpleServerImpl(IOptions<SimpleServerOptions> options, ILoggerFactory loggerFactory)
        {
            _listener = new HttpListener();
            _options = options.Value;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public async Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) where TContext : notnull
        {
            try
            {
                _listener.Prefixes.Add(_options.CodeBackedListenOption.ToUrl());
                _listener.Start();
                while (_listener.IsListening || !cancellationToken.IsCancellationRequested)
                {
                    var asyncResult = _listener.BeginGetContext(async ar =>
                    {
                        var listenerContext = _listener.EndGetContext(ar);
                        var context = application.CreateContext(new HttpProtocol(listenerContext));
                        await application.ProcessRequestAsync(context);
                    }, application);
                    asyncResult.AsyncWaitHandle.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IFeatureCollection Features { get; }
    }
}