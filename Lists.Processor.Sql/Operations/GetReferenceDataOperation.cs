using System.Collections.Generic;
using System.Data.Common;
using Lists.Processor.Sql.Models;

namespace Lists.Processor.Sql.Operations
{
    
    public class GetReferenceDataOperation : IDatabaseRetrievalOperation<IEnumerable<ReferenceData>>
    {
        private const string _query = 
        @"use temp;
        select * from ReferenceData;";
        public IEnumerable<ReferenceData> ResultOrDefault(DbDataReader reader) 
        {
            var list = new List<ReferenceData>();
            while(reader.Read()) 
            {
                var refData = new ReferenceData(reader);
                list.Add(refData);
            }

            return list;
        }

        public string QueryString()
        {
            return _query;
        }
    }
}