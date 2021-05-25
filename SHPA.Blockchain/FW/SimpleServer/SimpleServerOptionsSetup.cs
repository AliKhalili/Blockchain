using System;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.FW.SimpleServer
{
    internal class SimpleServerOptionsSetup : IConfigureOptions<SimpleServerOptions>
    {
        private readonly IServiceProvider _services;

        public SimpleServerOptionsSetup(IServiceProvider services)
        {
            _services = services;
        }

        public void Configure(SimpleServerOptions options)
        {
            options.ApplicationServices = _services;
        }
    }
}