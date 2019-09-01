using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lists.Processor.Sql;
using Lists.Processor.Caching;
using Lists.Processor.Options;
using Lists.Processor.Services;

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
                    configBuilder.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext, logging) => {
                    logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) => {
                    services.AddLogging();
                    services.AddHostedService<HostService>();

                    services.AddScoped(typeof(IIOService), typeof(HttpListenerService));

                    services.AddTransient(typeof(IDatabase), typeof(Database));
                    services.AddTransient(typeof(ICachingClient), typeof(CachingClient));

                    services.AddSingleton(typeof(IUtilityService), typeof(PingService));
                    services.AddSingleton(typeof(IUtilityService), typeof(ReferenceDataService));

                    services.Configure<RedisOptions>(hostContext.Configuration.GetSection("Redis"));
                    services.Configure<SqlOptions>(hostContext.Configuration.GetSection("MySql"));
                    services.Configure<HttpListenerOptions>(hostContext.Configuration.GetSection("Http"));
                })
                .Build();

            await host.RunAsync();
        }
    }
}