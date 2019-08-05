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
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogInformation("running host");
            _services = _serviceProvider.GetServices<IService>();
            foreach(var service in _services)
            {
                service.Start();
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken token)
        {
            foreach(var service in _services)
            {
                service.Stop();
            }
            return Task.CompletedTask;
        }
}
}