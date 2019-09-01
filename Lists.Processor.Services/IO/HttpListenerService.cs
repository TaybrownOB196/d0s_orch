using Lists.Processor.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Lists.Processor.Services
{
    public class HttpListenerService : BaseService, IIOService
    {
        private readonly IEnumerable<string> _prefixes;
        private readonly HttpListener _listener;
        public HttpListenerService(
            IOptionsMonitor<HttpListenerOptions> httpOption,
            ILogger<HttpListenerService> logger)
            : base(nameof(HttpListenerService), logger)
        {
            _prefixes = httpOption.CurrentValue.Prefixes;
            _listener = new HttpListener();
            foreach(var prefix in _prefixes)
            {
                _listener.Prefixes.Add(prefix);
            }
        }

        protected async override Task StartServiceAsync()
        {
            _listener.Start();
            await Task.Run(() => 
                _listener.BeginGetContext(ProcessAsync, _listener)).ConfigureAwait(false);
        }

        internal void ProcessAsync(IAsyncResult result) 
        {
            if (result.IsCompleted)
            {
                var listener = (HttpListener) result.AsyncState;
                listener.BeginGetContext(ProcessAsync, listener);

                HttpListenerContext context = listener.EndGetContext(result);
                var response = context.Response;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("{'success':'ok'}");
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        protected override Task StopServiceAsync()
        {
            _listener.Close();
            return Task.CompletedTask;
        }
    }

}