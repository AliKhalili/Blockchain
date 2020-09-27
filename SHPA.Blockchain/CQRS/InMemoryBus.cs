using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace SHPA.Blockchain.CQRS
{
    public class InMemoryBus : IMediatorHandler
    {
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider;

        public InMemoryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //_handler = new Dictionary<string, IRequestHandler<,>>();
            var type = typeof(IRequestHandler<,>);
            foreach (var innerType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()))
            {
                foreach (var singleHandler in innerType.GetInterfaces().Where(x => x.IsGenericType).Select(x => x.GetGenericTypeDefinition()))
                {
                    if (singleHandler == type && !string.IsNullOrEmpty(singleHandler.Name))
                    {
                        //var handlerMethods = singleHandler.GetMethods();
                        //var resolve = serviceProvider.GetService(innerType);
                        //_handler.Add(singleHandler.Name, (ICommandHandler<ICommand, IResponse>)serviceProvider.GetService(singleHandler));
                    }
                }
            }
        }

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : IResponse
        {
            var requestType = request.GetType();

            var obj = (RequestHandlerBase)Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<TRequest, TResponse>).MakeGenericType(requestType, typeof(TResponse)));
            //var handler = _requestHandlers.GetOrAdd(requestType, Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<TRequest, TResponse>).MakeGenericType(requestType, typeof(TResponse))));
            //return handler.Handle(command);
            //if (_handler.ContainsKey(requestType))
            //{
            //    return _handler[requestType].Handle(command);
            //}
            //return handler.Handle(request);
            try
            {
                var x = obj.Handle(request, _serviceProvider);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            throw new System.NotImplementedException();
        }

        public Task Publish(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}