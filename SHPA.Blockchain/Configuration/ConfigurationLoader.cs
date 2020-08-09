using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SHPA.Blockchain.Configuration
{
    /// <summary>
    /// A class for binding each configuration section in json to corresponded object
    /// </summary>
    public static class ConfigurationLoader
    {
        /// <summary>
        /// A extension method that use for configure Options
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static void LoadConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<NodeConfiguration>(configuration.GetSection("Node"));
        }
    }
}