using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Lists.Processor
{
    public class HostService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IService _service;
        public HostService(ILogger<HostService> logger, IService service) {
            _logger = logger;
            _service = service;
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("running host");
            _service.Start();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken token)
        {
            _service.Stop();
            return Task.CompletedTask;
        }
}
}