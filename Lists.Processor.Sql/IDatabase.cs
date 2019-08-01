using Lists.Processor.Sql.Models;
using Lists.Processor.Sql.Operations;

namespace Lists.Processor.Sql
{
    public interface IDatabase
    {
        T ExecuteQuery<T>(IDatabaseRetrievalOperation<T> operation) where T : DbModel;
    }
}