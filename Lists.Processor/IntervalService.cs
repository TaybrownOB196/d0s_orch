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
        protected TServiceInfo _trigger;

        protected IntervalService(string name, int timeout, int interval, ILogger logger)
            : base(name, logger)
        {
            _timeout = timeout;
            _interval = interval;
        }

        protected override void StartService() 
        {
            _trigger = new TServiceInfo(); 
            _timer = new Timer(
                ExecuteAsync, 
                _trigger, 
                _timeout, 
                _interval);
        } 
        protected override void StopService()
        { 
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