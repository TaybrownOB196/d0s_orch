using System.Data.Common;
using System;
using System.Collections.Generic;
using Lists.Processor.Sql.Models;

namespace Lists.Processor.Sql.Operations
{
    public class GetDosListsOperation : IDatabaseRetrievalOperation<IEnumerable<DosList>>
    {
        private const string _query = "use temp;select * from List;";
        public IEnumerable<DosList> ResultOrDefault(DbDataReader reader) 
        {
            var list = default(List<DosList>);
            while(reader.Read()) 
            {
                list.Add(new DosList(reader));
            }

            return list;
        }

        public string QueryString()
        {
            return _query;
        }
    }
}