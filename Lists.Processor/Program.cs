using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Lists.Processor
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureServices((hostContext, serivces) => 
            {
                
            });

            await builder.RunConsoleAsync();
        }
    }
}
