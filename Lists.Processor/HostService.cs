using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Collections.Generic;
using System;

namespace Lists.Processor
{
    public class HostService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private IEnumerable<IService> _services;
        public HostService(IServiceProvider serviceProvider, ILogger<HostService> logger) {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _services = _serviceProvider.GetServices<IService>();
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("running host");
            Parallel.ForEach(_services, async (service) => {
                await service.PrestartAsync().ConfigureAwait(false);
            });
            Parallel.ForEach(_services, async (service) => {
                await service.StartAsync().ConfigureAwait(false);
            });

            return Task.CompletedTask;
        }

        public async override Task StopAsync(CancellationToken token)
        {
            List<Task> preStopTasks = new List<Task>();
            List<Task> stopTasks = new List<Task>();

            foreach(var service in _services) 
            {
                preStopTasks.Add(service.StopAsync());
            }
            await Task.WhenAll(preStopTasks.ToArray());

            foreach(var service in _services) 
            {
                stopTasks.Add(service.StopAsync());
            }
            await Task.WhenAll(stopTasks.ToArray());
        }
    }
}