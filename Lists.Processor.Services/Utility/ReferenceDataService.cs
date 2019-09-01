using Lists.Processor.Sql;
using Lists.Processor.Sql.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Lists.Processor.Services
{
    public interface IReferenceDataProvider
    {
        T ValueOrDefault<T>(string key, T defaultValue);

        void Reload();
    }

    public class ReferenceDataService : BaseService, IUtilityService, IReferenceDataProvider
    {
        private readonly IDatabase _db;
        private ConcurrentDictionary<string, string> _data;
        public ReferenceDataService(IDatabase database, ILogger<ReferenceDataService> logger)
            : base(nameof(ReferenceDataService), logger)
        {
            _db = database;
            _data = new ConcurrentDictionary<string, string>();
        }
        protected override Task StartServiceAsync() 
        {
            Reload();
            return Task.CompletedTask;            
        }
        protected override Task StopServiceAsync() 
        {
            return Task.CompletedTask;            
        }

        public void Reload()
        {
            var reloadDict = new ConcurrentDictionary<string, string>();
            var operation = new GetReferenceDataOperation();
            var referenceData = _db.ExecuteQuery(operation);
            foreach(var refData in referenceData)
            {
                _logger.LogInformation($"key: {refData.key}, value: {refData.value}");
                reloadDict.TryAdd(refData.key, refData.value);
            }
            _data = reloadDict;
        }

        public T ValueOrDefault<T>(string key, T defaultValue)
        {
            try 
            {
                if (_data.TryGetValue(key, out var value)) 
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return defaultValue;
        }
    }
}