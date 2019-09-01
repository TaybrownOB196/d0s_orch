using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Collections.Generic;
using System;
using Lists.Processor.Services;

namespace Lists.Processor
{
    public class HostService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private IEnumerable<IIOService> _ioServices;
        private IEnumerable<IUtilityService> _utilityServices;
        public HostService(IServiceProvider serviceProvider, ILogger<HostService> logger) {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _ioServices = _serviceProvider.GetServices<IIOService>();
            _utilityServices = _serviceProvider.GetServices<IUtilityService>();
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("starting host");
            _logger.LogDebug("starting io services");
            Parallel.ForEach(_ioServices, async (service) => {
                await service.StartAsync();
            });
            
            _logger.LogDebug("starting utility services");
            Parallel.ForEach(_utilityServices, async (service) => {
                await service.StartAsync().ConfigureAwait(false);
            });

            return Task.CompletedTask;
        }

        public async override Task StopAsync(CancellationToken token)
        {
            List<Task> usTasks = new List<Task>();
            _logger.LogDebug("stopping utility services");
            foreach(var service in _utilityServices) 
            {
                usTasks.Add(service.StopAsync());
            }
            await Task.WhenAll(usTasks.ToArray());

            List<Task> sTasks = new List<Task>();
            _logger.LogDebug("stopping io services");
            foreach(var service in _ioServices) 
            {
                sTasks.Add(service.StopAsync());
            }
            await Task.WhenAll(sTasks.ToArray());
        }
    }
}