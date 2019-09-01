using Microsoft.Extensions.Logging;
using Lists.Processor.Caching;
using System.Threading.Tasks;

namespace Lists.Processor.Services
{
    public class PingService : IntervalService, IUtilityService
    {
        private readonly ICachingClient _cacheClient;
        public PingService(ILogger<PingService> logger, ICachingClient cacheClient) 
            : base(nameof(PingService), 1000, logger) 
            {
                _cacheClient = cacheClient;
            }

        protected override Task ExecuteAsync()
        {
            _logger.LogInformation("ping");
            _cacheClient.Ping();
            return Task.CompletedTask;
        }
    }

}