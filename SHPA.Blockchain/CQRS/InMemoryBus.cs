using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHPA.Blockchain.CQRS
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly Dictionary<string, ICommandHandler<ICommand, IResponse>> _handler;

        public InMemoryBus(IServiceProvider serviceProvider)
        {
            _handler = new Dictionary<string, ICommandHandler<ICommand, IResponse>>();
            var type = typeof(ICommandHandler<,>);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).ToArray();
            foreach (var p in types)
            {
                if (p.Name == "CommandHandler")
                {
                    Console.WriteLine(p.Name);
                    var interfaces = p.GetInterfaces();
                    if (type.IsAssignableFrom(p))
                    {

                    }
                }
            }
            var findHandlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface).ToList();
            foreach (var commandHandler in findHandlers)
            {
                _handler.Add(commandHandler.Name, (ICommandHandler<ICommand, IResponse>)serviceProvider.GetService(commandHandler));
            }
        }

        public Task<IResponse> Send(ICommand command)
        {
            var requestType = command.GetType();
            if (_handler.ContainsKey(requestType))
            {
                return _handler[requestType].Handle(command);
            }
            throw new NotImplementedException();
        }

        public Task Publish(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}