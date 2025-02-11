﻿using Microsoft.Extensions.Options;
using SHPA.Blockchain.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;

namespace SHPA.Blockchain.Server
{
    /// <summary>
    /// A tiny web server in order to handle REST api request, implemented by HttpListenerRequest
    /// </summary>
    public class EmbeddedRestServer 
    {
        private readonly IRequestHandler _requestHandler;
        private readonly NodeConfiguration _option;
        private HttpListener _listener;
        private readonly Thread[] _workers;
        private ConcurrentQueue<HttpListenerContext> _queue;
        private readonly ManualResetEvent _stop, _ready;

        public EmbeddedRestServer(IOptions<NodeConfiguration> option, IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            _option = option.Value;
            _workers = new Thread[_option.MaxThread];
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _queue = new ConcurrentQueue<HttpListenerContext>();
        }
        public void Start(CancellationToken cancellationToken)
        {
            if (_listener != null)
                throw new InvalidOperationException("server is already running");
            _listener = new HttpListener();
            _listener.Prefixes.Add($"{_option.GetFullAddress()}/");
            _listener.Start();
            Console.WriteLine($"node {_option.Name} start on {_option.GetFullAddress()} at {DateTime.UtcNow:s}");

            Task.Run(() =>
            {
                while (_listener.IsListening)
                {
                    var context = _listener.BeginGetContext(ContextReady, null);

                    if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                        return;
                }
            }, cancellationToken);

            for (int i = 0; i < _workers.Length; i++)
            {
                _workers[i] = new Thread(Worker);
                _workers[i].Start();
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            _queue.Enqueue(_listener.EndGetContext(ar));
            _ready.Set();
        }
        private void Worker()
        {
            WaitHandle[] wait = { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                if (_queue.TryDequeue(out var context))
                {
                    _requestHandler.HandleAsync(context);
                }
                else
                    _ready.Reset();
            }
        }
    }
}