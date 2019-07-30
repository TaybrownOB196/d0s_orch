using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lists.Processor.Caching;
using Lists.Processor.Options;

namespace Lists.Processor
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configBuilder) => {
                    configBuilder.AddJsonFile("appsettings.json");
                    configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configBuilder.AddEnvironmentVariables();
                    configBuilder.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext, logging) => {
                    logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) => {
                    services.AddHostedService<HostService>();
                    services.AddSingleton(typeof(IService), typeof(PingService));
                    services.AddSingleton(typeof(ICachingClient), typeof(CachingClient));
                    services.Configure<RedisOptions>(hostContext.Configuration.GetSection("Redis"));
                })
                .Build();

            await host.RunAsync();
        }
    }
}
