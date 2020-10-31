using Microsoft.Extensions.Options;
using SHPA.Blockchain.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SHPA.Blockchain.Server
{
    /// <summary>
    /// A tiny web server in order to handle REST api request, implemented by HttpListenerRequest
    /// </summary>
    public class EmbeddedRestServer : IServer
    {
        private readonly IRequestHandler _requestHandler;
        private readonly NodeConfiguration _option;
        private HttpListener _listener;
        private readonly Thread[] _workers;
        private Queue<HttpListenerContext> _queue;
        private readonly ManualResetEvent _stop, _ready;
        private CancellationToken _cancellationToken;

        public EmbeddedRestServer(IOptions<NodeConfiguration> option, IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            _option = option.Value;
            _workers = new Thread[_option.MaxThread];
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _queue = new Queue<HttpListenerContext>();
        }
        public void Start(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
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
            try
            {
                lock (_queue)
                {
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }
        private void Worker()
        {
            WaitHandle[] wait = { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerContext context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        context = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try
                {
                    _requestHandler.HandleAsync(context);
                }
                catch (Exception e) { Console.Error.WriteLine(e); }
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            _stop.Set();
            foreach (Thread worker in _workers)
                worker.Join();
            _listener.Stop();
        }
    }
}