﻿using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SHPA.Blockchain.Blocks;
using SHPA.Blockchain.Configuration;
using SHPA.Blockchain.Server;
using SHPA.Blockchain.Server.Actions;

namespace SHPA.Blockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var cancellationTokenSource = new CancellationTokenSource();
            serviceProvider.GetService<Application>().Run(cancellationTokenSource);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            var configuration = builder.Build();

            serviceCollection.LoadConfiguration(configuration);

            serviceCollection.AddTransient<Application>();
            serviceCollection.AddSingleton<IBlockchain, Blocks.Blockchain>();
            serviceCollection.AddTransient<IServer, EmbeddedRestServer>();
            serviceCollection.AddTransient<IRequestHandler, RestHandler>();
            serviceCollection.AddTransient<IActionFactory, ActionFactory>();
            serviceCollection.AddTransient<IProofOfWork, DefaultProofOfWork>();
            serviceCollection.AddTransient<AddTransactionAction>();
            serviceCollection.AddTransient<MineAction>();
            serviceCollection.AddTransient<ChainAction>();
            serviceCollection.AddTransient<ValidateChainAction>();
        }
    }
}
