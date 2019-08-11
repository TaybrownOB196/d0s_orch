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

        //Not used
        public void Prestart() { }
        //Not used
        public void Prestop() { }

        public void Start()
        {
            _logger.LogDebug($"starting {_name}");
            StartService();
        }

        public void Stop()
        {
            _logger.LogDebug($"stopping {_name}");
        }
        
        protected abstract void StartService();
        protected abstract void StopService();
    }
}