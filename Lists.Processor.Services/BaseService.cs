using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Lists.Processor.Services
{
    public abstract class BaseService : IService
    {
        protected readonly string _name; 
        protected readonly ILogger _logger;
        protected BaseService(string name, ILogger logger)
        {
            _name = name;
            _logger = logger;
        }

        public async Task StartAsync()
        {
            _logger.LogDebug($"starting {_name}");
            await StartServiceAsync();
        }
        
        public async Task StopAsync()
        {
            _logger.LogDebug($"stopping {_name}");
            await StopServiceAsync();
        }

        protected abstract Task StartServiceAsync();
        protected abstract Task StopServiceAsync();
    }
}