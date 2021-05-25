using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SHPA.Blockchain.Actions;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Client;
using SHPA.Blockchain.Configuration;
using SHPA.Blockchain.Nodes;
using SHPA.Blockchain.Server;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SHPA.Blockchain.CQRS;
using SHPA.Blockchain.CQRS.Bus;
using SHPA.Blockchain.CQRS.Domain;
using SHPA.Blockchain.CQRS.Domain.Commands;
using SHPA.Blockchain.FW.SimpleServer;
using SHPA.Blockchain.Server.Actions.Custom;

namespace SHPA.Blockchain
{
    class Program
    {
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }
        //static async Task Main(string[] args)
        //{
        //    //HttpServer server = new HttpServer();
        //    //await server.StartAsync(new DummyApplication(context =>
        //    //{
        //    //    var processTime = new Random().Next(5);
        //    //    processTime = 4;
        //    //    var requestId = Guid.NewGuid().ToString().Replace("-","").ToLower();
        //    //    Console.WriteLine("{2:T}-start process request {1} and process time is {0}s", processTime, requestId, DateTimeOffset.UtcNow);
        //    //    Thread.Sleep(processTime * 1000);
        //    //    Console.WriteLine("{2:T}-finish process request {1} and process time is {0}s", processTime, requestId, DateTimeOffset.UtcNow);
        //    //    return Task.CompletedTask;
        //    //}), CancellationToken.None);
        //    //var serviceCollection = new ServiceCollection();
        //    //ConfigureServices(serviceCollection);

        //    //var serviceProvider = serviceCollection.BuildServiceProvider();

        //    //var cancellationTokenSource = new CancellationTokenSource();
        //    //serviceProvider.GetService<Application>().Run(cancellationTokenSource);
        //}

        //private static void ConfigureServices(IServiceCollection serviceCollection)
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory))
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        //    var configuration = builder.Build();

        //    serviceCollection.LoadConfiguration(configuration);

        //    serviceCollection.AddTransient<Application>();
        //    serviceCollection.AddSingleton<IEngine, Engine>();
        //    serviceCollection.AddSingleton<IMediatorHandler, InMemoryBus>();
        //    serviceCollection.AddTransient<IBlockchain, Blocks.Blockchain>();
        //    serviceCollection.AddTransient<IQueryService, QueryService>();
        //    serviceCollection.AddTransient<INodeManager, NodeManager>();
        //    serviceCollection.AddTransient<IServer, EmbeddedRestServer>();
        //    serviceCollection.AddTransient<IRequestHandler, RestHandler>();
        //    serviceCollection.AddTransient<IActionFactory, ActionFactory>();
        //    serviceCollection.AddTransient<IProofOfWork, DefaultProofOfWork>();
        //    serviceCollection.AddTransient<RestClient>();
        //    serviceCollection.AddTransient<AddTransactionAction>();
        //    serviceCollection.AddTransient<MineAction>();
        //    serviceCollection.AddTransient<PingAction>();
        //    serviceCollection.AddTransient<ChainAction>();
        //    serviceCollection.AddTransient<FakeLoadAction>();
        //    serviceCollection.AddTransient<ValidateChainAction>();
        //    serviceCollection.AddTransient<RegisterNodeAction>();
        //    serviceCollection.AddTransient<GetRegisterNodeAction>();
        //    serviceCollection.AddTransient<AddBlockAction>();
        //    serviceCollection.AddTransient<IRequestHandler<AddBlockCommand, DefaultResponse>, RequestHandler>();
        //    serviceCollection.AddTransient<IRequestHandler<AddTransactionCommand, DefaultResponse>, RequestHandler>();
        //}


        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {

            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.Run(async context =>
                {
                    Thread.Sleep(2000);
                    var response = $"{DateTimeOffset.UtcNow.Ticks}- hello, world{Environment.NewLine}";
                    context.Response.ContentLength = response.Length;
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(response);
                });
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseSimpleServer((context, options) =>
                    {
                        options.Listen(IPAddress.Parse("127.0.0.1"), 8080);
                    });
                    webHostBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                });
    }
}
