using Microsoft.Extensions.Options;
using Lists.Processor.Options;
using ServiceStack.Redis;

namespace Lists.Processor.Caching
{
    public class CachingClient : ICachingClient
    {
        private readonly RedisManagerPool _manager;

        public CachingClient(IOptionsMonitor<RedisOptions> redisOption) 
        {
            _manager = new RedisManagerPool(redisOption.CurrentValue.ConnectionString);
        }

        public bool Ping() 
        {
            using (var client = _manager.GetClient())
            {
                return client.Ping();
            }
        }
    }
}
