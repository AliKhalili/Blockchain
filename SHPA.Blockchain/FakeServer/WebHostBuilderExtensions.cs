using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SHPA.Blockchain.FakeServer.Core;
using SHPA.Blockchain.FakeServer.Core.Internal;

namespace SHPA.Blockchain.FakeServer
{
    /// <summary>
    /// Fake Server <see cref="IWebHostBuilder"/> extensions.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Specify Fake Server as the server to be used by the web host.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder to configure.
        /// </param>
        /// <returns>
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
        /// </returns>
        public static IWebHostBuilder UseFakeServer(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IConfigureOptions<FakeServerOptions>, FakeServerOptionsSetup>();
                services.AddSingleton<IServer, FakeServerImpl>();
            });
        }

        /// <summary>
        /// Specify Kestrel as the server to be used by the web host.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder to configure.
        /// </param>
        /// <param name="options">
        /// A callback to configure Kestrel options.
        /// </param>
        /// <returns>
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
        /// </returns>
        public static IWebHostBuilder UseFakeServer(this IWebHostBuilder hostBuilder, Action<FakeServerOptions> options)
        {
            return hostBuilder.UseKestrel().ConfigureFakeServer(options);
        }

        /// <summary>
        /// Configures Kestrel options but does not register an IServer. See <see cref="UseFakeServer(IWebHostBuilder)"/>.
        /// </summary>
        /// <param name="hostBuilder">
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder to configure.
        /// </param>
        /// <param name="options">
        /// A callback to configure Kestrel options.
        /// </param>
        /// <returns>
        /// The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
        /// </returns>
        public static IWebHostBuilder ConfigureFakeServer(this IWebHostBuilder hostBuilder, Action<FakeServerOptions> options)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.Configure(options);
            });
        }
    }
}