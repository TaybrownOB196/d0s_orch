using System.Data.Common;
using Lists.Processor.Sql.Models;

namespace Lists.Processor.Sql.Operations
{
    public class GetDosListsOperation : IDatabaseRetrievalOperation<DosList>
    {
        private const string _query = "use temp;select * from List;";
        public DosList ResultOrDefault(DbDataReader reader) 
        {
            return new DosList(reader);
        }

        public string QueryString()
        {
            return _query;
        }
    }
}