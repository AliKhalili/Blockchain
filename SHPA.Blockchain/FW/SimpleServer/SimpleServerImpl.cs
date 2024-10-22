﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.FW.SimpleServer
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
                var url = _options.CodeBackedListenOption.ToUrl();
                _listener.Prefixes.Add(_options.CodeBackedListenOption.ToUrl());
                _listener.Start();
                _logger.LogInformation("Now listening on: {0}", url);
                _ = Task.Run(() =>
                  {
                      while (_listener.IsListening || !cancellationToken.IsCancellationRequested)
                      {
                          var asyncResult = _listener.BeginGetContext(async ar =>
                          {
                              var listenerContext = _listener.EndGetContext(ar);
                              var context = application.CreateContext(new HttpProtocol(listenerContext));
                              await application.ProcessRequestAsync(context);
                              listenerContext.Response.OutputStream.Close();
                          }, application);
                          asyncResult.AsyncWaitHandle.WaitOne();
                      }
                  }, cancellationToken);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "an exception has occurred in StartAsync method");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public void Dispose()
        {
        }

        public IFeatureCollection Features { get; }
    }
}