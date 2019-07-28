using Microsoft.Extensions.Logging;
using System.Threading;
using System;

namespace Lists.Processor
{
    public abstract class IntervalService<TTrigger> : IService
    {
        private Timer _timer;
        private readonly int _timeout;
        private readonly int _interval;
        private readonly TTrigger _trigger;
        protected readonly ILogger _logger;
        protected readonly string _name;

        protected IntervalService(string name, int timeout, int interval, TTrigger trigger, ILogger logger) 
        {
            _name = name;
            _timeout = timeout;
            _interval = interval;
            _trigger = trigger;
            _logger = logger;
        }

        public void Prestart() { }
        public void Prestop() { }
        public void Start() 
        {
            _logger.LogDebug($"init {_name} timer");
            _timer = new Timer(
                ExecuteAsync, 
                _trigger, 
                _timeout, 
                _interval);
        } 
        public void Stop()
        { 
            _logger.LogDebug($"disposing {_name} timer");
            _timer.Dispose();
        }

        private void ExecuteAsync(Object obj) 
        {
            ExecuteAsync((TTrigger)obj);
        }
        protected abstract void ExecuteAsync(TTrigger trigger);
    }
}