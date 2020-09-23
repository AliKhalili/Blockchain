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
        private readonly Dictionary<string, ICommandHandler<ICommand, IResponse>> _handler;
        private readonly IServiceProvider _serviceProvider;

        public InMemoryBus(IServiceProvider serviceProvider)
        {
            _handler = new Dictionary<string, ICommandHandler<ICommand, IResponse>>();
            var type = typeof(ICommandHandler<,>);
            foreach (var innerType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()))
            {
                foreach (var singleHandler in innerType.GetInterfaces().Where(x=>x.IsGenericType).Select(x=>x.GetGenericTypeDefinition()))
                {
                    if(singleHandler == type && !string.IsNullOrEmpty(singleHandler.Name))
                        _handler.Add(singleHandler.Name, (ICommandHandler<ICommand, IResponse>)serviceProvider.GetService(singleHandler));
                }
            }
        }

        public Task<IResponse> Send(ICommand command)
        {
            var requestType = command.GetType();

            //var handler = (ICommandHandler<ICommand, IResponse>)_requestHandlers.GetOrAdd(requestType.ToString(),
            //    t => Activator.CreateInstance(requestType));

            //var handler = (ICommandHandler<ICommand, IResponse>)_requestHandlers.GetOrAdd(requestType.ToString(),
            //    _serviceProvider.GetService(ICommandHandler<,>);

            //return handler.Handle(command);
            //if (_handler.ContainsKey(requestType))
            //{
            //    return _handler[requestType].Handle(command);
            //}
            throw new NotImplementedException();
        }

        public Task Publish(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}