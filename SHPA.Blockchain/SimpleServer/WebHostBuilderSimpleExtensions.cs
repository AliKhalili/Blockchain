using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;

namespace SHPA.Blockchain.SimpleServer
{
    public static class WebHostBuilderSimpleExtensions
    {
        public static IWebHostBuilder UseSimpleServer(this IWebHostBuilder hostBuilder, Action<WebHostBuilderContext, SimpleServerOptions> configureOptions)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IServer, SimpleServerImpl>();
                services.Configure<SimpleServerOptions>(options =>
                {
                    configureOptions(context, options);
                });
            });
        }
    }
}