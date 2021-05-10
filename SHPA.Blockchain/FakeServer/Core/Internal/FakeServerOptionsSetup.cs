using System;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.FakeServer.Core.Internal
{
    public class FakeServerOptionsSetup : IConfigureOptions<FakeServerOptions>
    {
        private readonly IServiceProvider _services;

        public FakeServerOptionsSetup(IServiceProvider services)
        {
            _services = services;
        }
        public void Configure(FakeServerOptions options)
        {
            options.ApplicationServices = _services;
        }
    }
}