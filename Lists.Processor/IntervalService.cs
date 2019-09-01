using Microsoft.Extensions.Logging;
using System.Timers;
using System;
using System.Threading.Tasks;

namespace Lists.Processor
{
    public abstract class IntervalService : BaseService
    {
        private Timer _timer;
        private readonly int _interval;

        protected IntervalService(string name, int interval, ILogger logger)
            : base(name, logger)
        {
            _interval = interval;
        }

        public override Task StartAsync() 
        {
            _timer = new Timer(_interval);
            _timer.Elapsed += async (sender, e) => await ExecuteAsync(sender, e);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            return Task.CompletedTask;
        }

        public override Task StopAsync()
        {
            _timer.Stop();
            return Task.CompletedTask;
        }

        private async Task ExecuteAsync(Object source, ElapsedEventArgs e) 
        {
            await ExecuteAsync().ConfigureAwait(false);
        }
        protected abstract Task ExecuteAsync();
    }
}