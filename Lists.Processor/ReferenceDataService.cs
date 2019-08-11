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
        private Dictionary<string, string> _data;
        public ReferenceDataService(IDatabase database, ILogger<ReferenceDataService> logger)
            : base(nameof(ReferenceDataService), logger)
        {
            _db = database;
            _data = new Dictionary<string, string>();
        }
        protected override void StartService() 
        {
            Reload();
        }
        protected override void StopService() 
        {
            _data = new Dictionary<string, string>();
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