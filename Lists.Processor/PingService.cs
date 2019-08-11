
using Microsoft.Extensions.Logging;
using Lists.Processor.Caching;

namespace Lists.Processor
{
    public class ServiceInfo { }
    public class PingService : IntervalService<ServiceInfo>
    {
        private readonly ICachingClient _cacheClient;
        public PingService(ILogger<PingService> logger, ICachingClient cacheClient) 
            : base(nameof(PingService), 0, 1000, logger) 
            {
                _cacheClient = cacheClient;
            }

        protected override void ExecuteAsync(ServiceInfo trigger)
        {
            _logger.LogInformation("ping");
            _cacheClient.Ping();
        }
    }

}