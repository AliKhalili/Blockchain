using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHPA.Blockchain.FakeServer.Core.Internal.Connections
{
    public class DefaultConnectionBuilder:IConnectionBuilder
    {
        private readonly List<Func<ConnectionDelegate, ConnectionDelegate>> _middleware = new();

        public IServiceProvider ApplicationServices { get; }

        public DefaultConnectionBuilder(IServiceProvider applicationServices)
        {
            ApplicationServices = applicationServices;
        }
        public IConnectionBuilder Use(Func<ConnectionDelegate, ConnectionDelegate> middleware)
        {
            _middleware.Add(middleware);
            return this;
        }

        /// <summary>
        /// Builds the <see cref="ConnectionDelegate"/>.
        /// </summary>
        /// <returns>The <see cref="ConnectionDelegate"/>.</returns>
        public ConnectionDelegate Build()
        {
            ConnectionDelegate app = _ => Task.CompletedTask;
            foreach (var component in _middleware)
            {
                app = component(app);
            }
            return app;
        }
    }
}