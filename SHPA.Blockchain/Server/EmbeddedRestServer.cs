using Microsoft.Extensions.Options;
using SHPA.Blockchain.Configuration;
using System;
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

        public EmbeddedRestServer(IOptions<NodeConfiguration> option, IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            _option = option.Value;
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
                while (true)
                {
                    _listener.GetContextAsync().ContinueWith(ContinuationAction, cancellationToken)
                        .Wait(cancellationToken);
                }
            }, cancellationToken);

        }

        private void ContinuationAction(Task<HttpListenerContext> taskListener)
        {
            _requestHandler.HandleAsync(taskListener);
        }

        public void Stop()
        {

        }
    }
}