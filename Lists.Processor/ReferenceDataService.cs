using Lists.Processor.Sql;
using Lists.Processor.Sql.Operations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace Lists.Processor
{
    public class ReferenceDataService : BaseService, IReferenceDataProvider
    {
        private readonly IDatabase _db;
        private readonly ILogger _logger;
        private Dictionary<string, string> _data;
        public ReferenceDataService(IDatabase database, ILogger<ReferenceDataService> logger)
            : base(nameof(ReferenceDataService))
        {
            _db = database;
            _logger = logger;
            _data = new Dictionary<string, string>();
        }
        public override void Start() 
        {
            _logger.LogDebug($"starting {_name}");
            Reload();
        }
        public override void Stop() 
        {
            _logger.LogDebug($"stopping {_name}");
        }

        public void Reload()
        {
            var operation = new GetReferenceDataOperation();
            var referenceData = _db.ExecuteQuery(operation);
            foreach(var refData in referenceData)
            {
                _logger.LogInformation($"key: {refData.key}, value: {refData.value}");
                _data.Add(refData.key, refData.value);
            }
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