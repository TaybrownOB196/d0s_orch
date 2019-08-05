using Lists.Processor.Sql.Models;
using System.Collections.Generic;
using System.Data.Common;

namespace Lists.Processor.Sql.Operations
{
    public interface IDatabaseRetrievalOperation<T> : IDatabaseOperation where T : IEnumerable<DbModel>
    {
        T ResultOrDefault(DbDataReader reader);
    }
}