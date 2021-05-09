using System.Collections.Concurrent;

namespace SHPA.Blockchain.Server.Internal
{
    public class GenericPool<T> where T : class
    {
        private readonly ConcurrentQueue<T> _queue;
        private readonly int _maxQueueSize;

        /// <summary>
        /// create instance of <see cref="GenericPool{T}"/>.
        /// </summary>
        /// <param name="maxQueueSize">The size of the pool</param>
        public GenericPool(int maxQueueSize = 1024)
        {
            _queue = new();
            _maxQueueSize = maxQueueSize;
        }

        /// <summary>
        /// add new object to the pool
        /// </summary>
        /// <param name="item">A <typeparamref name="T"/>.</param>
        /// <returns>if pool did not exceed capacity which set in <see cref="_maxQueueSize"/> then return true </returns>
        public bool Enqueue(T item)
        {
            if (_queue.Count < _maxQueueSize)
            {
                _queue.Enqueue(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets an object from the pool if one is available, otherwise return null.
        /// </summary>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public T Dequeue()
        {
            if (_queue.TryDequeue(out var item))
            {
                return item;
            }

            return null;
        }
    }
}