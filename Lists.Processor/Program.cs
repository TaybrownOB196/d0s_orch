using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lists.Processor
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configBuilder) => {
                    configBuilder.AddJsonFile("appsettings.json");
                    configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment}.json", optional: true);
                    configBuilder.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext, logging) => {
                    logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) => {
                    services.AddHostedService<HostService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
