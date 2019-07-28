using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Lists.Processor
{
    public class HostService : BackgroundService
    {
        private readonly ILogger _logger;
        public HostService(ILogger<HostService> logger) {
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("running host");
            Task.Delay(1000);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
}
}