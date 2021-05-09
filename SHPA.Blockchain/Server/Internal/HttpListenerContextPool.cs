using System.Collections.Concurrent;
using System.Net;

namespace SHPA.Blockchain.Server.Internal
{
    public class HttpListenerContextPool
    {
        private readonly ConcurrentQueue<HttpListenerContext> _queue;
        private readonly int _maxQueueSize;
        public HttpListenerContextPool(int maxQueueSize = 1024)
        {
            _queue = new();
            _maxQueueSize = maxQueueSize;
        }

        public bool Enqueue(HttpListenerContext httpListenerContext)
        {
            if (_queue.Count < _maxQueueSize)
            {
                _queue.Enqueue(httpListenerContext);
                return true;
            }

            return false;
        }

        public HttpListenerContext Dequeue()
        {
            if (_queue.TryDequeue(out var item))
            {
                return item;
            }

            return null;
        }
    }
}