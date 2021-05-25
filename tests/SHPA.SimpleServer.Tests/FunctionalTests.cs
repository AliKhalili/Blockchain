using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SHPA.Blockchain.FW.SimpleServer;

namespace SHPA.SimpleServer.Tests
{
    public class FunctionalTests
    {
        public class TestStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {

            }

            public void Configure()
            {
            }
        }
        public static IHostBuilder GetHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseSimpleServer((context, options) =>
                    {
                        options.Listen(IPAddress.Parse("127.0.0.1"), 8080);
                    }).UseStartup<TestStartup>()
                        .Configure(app =>
                        {
                            app.Run(async context =>
                            {
                                Console.WriteLine(context.Request.QueryString);
                                var response = $"hello, world{Environment.NewLine}";
                                context.Response.ContentLength = response.Length;
                                context.Response.ContentType = "text/plain";
                                context.Response.StatusCode = 200;
                                await context.Response.WriteAsync(response);
                                Console.WriteLine("waiting for another request");
                            });
                        });
                });
        }

        [Fact]
        public async Task RequestSimpleHttpGet_OkStatus()
        {
            using var host = GetHostBuilder().Build();
            using var client = new HttpClient();
            host.Start();
            var response = await client.GetAsync($"http://127.0.0.1:8080/");
            response.EnsureSuccessStatusCode();
            await host.StopAsync();
        }

    }
}