using Lists.Processor.Sql.Models;
using System.Data.Common;

namespace Lists.Processor.Sql.Operations
{
    public interface IDatabaseRetrievalOperation<T> : IDatabaseOperation where T : DbModel
    {
        T ResultOrDefault(DbDataReader reader);
    }
}