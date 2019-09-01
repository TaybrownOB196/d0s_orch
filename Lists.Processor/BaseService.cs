using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Lists.Processor
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

        public virtual Task PrestartAsync()
        {
            _logger.LogDebug($"starting {_name}");
            return Task.CompletedTask;
        }

        public virtual Task PrestopAsync()
        {
            _logger.LogDebug($"stopping {_name}");
            return Task.CompletedTask;
        }

        public abstract Task StartAsync();
        public abstract Task StopAsync();
    }
}