using Microsoft.Extensions.Logging;
using System.Threading;
using System;

namespace Lists.Processor
{
    public abstract class IntervalService<TServiceInfo> : BaseService where TServiceInfo : new ()
    {
        private Timer _timer;
        private readonly int _timeout;
        private readonly int _interval;
        protected readonly ILogger _logger;
        protected TServiceInfo _trigger;

        protected IntervalService(string name, int timeout, int interval, ILogger logger)
            : base(name)
        {
            _timeout = timeout;
            _interval = interval;
            _logger = logger;
        }

        public override void Start() 
        {
            _logger.LogDebug($"init {_name} timer");
            _trigger = new TServiceInfo(); 
            _timer = new Timer(
                ExecuteAsync, 
                _trigger, 
                _timeout, 
                _interval);
        } 
        public override void Stop()
        { 
            _logger.LogDebug($"disposing {_name} timer");
            _timer.Change(0,0);
            _timer.Dispose();
        }

        private void ExecuteAsync(Object obj) 
        {
            ExecuteAsync((TServiceInfo)obj);
        }
        protected abstract void ExecuteAsync(TServiceInfo trigger);
    }
}