//using System;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using SHPA.Blockchain.Server.Internal;

//namespace SHPA.Blockchain.Server
//{
//    public class HttpServer : IServer
//    {
//        private readonly HttpListener _listener;
//        private readonly GenericPool<HttpListenerContext> _requestPool;
//        public HttpServer()
//        {
//            _listener = new HttpListener();
//            _listener.Prefixes.Add("http://localhost:8081/");
//            _requestPool = new GenericPool<HttpListenerContext>();
//        }

//        public IFeatureCollection Features { get; }
//        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) where TContext : notnull
//        {
//            _listener.Start();
//            while (_listener.IsListening)
//            {
//                var asyncResult = _listener.BeginGetContext(ar =>
//                {
//                    var context = _listener.EndGetContext(ar);
//                    if (!_requestPool.Enqueue(context))
//                    {
//                        //Todo:return HTTP error 503 service unavailable
//                        throw new Exception();
//                    }
//                }, application);
//                asyncResult.AsyncWaitHandle.WaitOne();
//            }
//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _listener.Stop();
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}