using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.FW.SimpleServer
{
    public static class WebHostBuilderSimpleExtensions
    {
        public static IWebHostBuilder UseSimpleServer(this IWebHostBuilder hostBuilder, Action<WebHostBuilderContext, SimpleServerOptions> configureOptions)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<IConfigureOptions<SimpleServerOptions>, SimpleServerOptionsSetup>();
                services.AddSingleton<IServer, SimpleServerImpl>();
                services.Configure<SimpleServerOptions>(options =>
                {
                    configureOptions(context, options);
                });
            });
        }
    }
}