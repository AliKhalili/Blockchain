using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SHPA.Blockchain.SimpleServer
{
    public static class WebHostBuilderSimpleExtensions
    {
        public static IWebHostBuilder UseSimpleServer(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IConfigureOptions<SimpleServerOptions>, SimpleServerOptionsSetup>();
                services.AddSingleton<IServer, SimpleServerImpl>();
            });
        }
    }
}