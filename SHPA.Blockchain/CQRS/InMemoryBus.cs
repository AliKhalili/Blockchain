using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly ConcurrentDictionary<Type, (object Object, MethodInfo Method)> _requestHandlers;
        private readonly IServiceProvider _serviceProvider;

        public InMemoryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _requestHandlers = new ConcurrentDictionary<Type, (object, MethodInfo)>();
        }

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse
        {
            var requestType = request.GetType();
            var (constructedType, method) = _requestHandlers.GetOrAdd(requestType, type =>
            {
                var genericType = typeof(RequestHandlerWrapper<,>).MakeGenericType(type, typeof(TResponse));
                var ct = Activator.CreateInstance(genericType);
                var m = genericType.GetMethod("Handle");
                return (ct, m);
            });
            return (Task<TResponse>)method.Invoke(constructedType, new object[] { request, _serviceProvider });
        }

        public Task Publish(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}