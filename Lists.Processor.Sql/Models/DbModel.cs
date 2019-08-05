using System.Data.Common;
using System;

namespace Lists.Processor.Sql.Models
{
    public abstract class DbModel
    {
        protected DbModel(DbDataReader reader) 
        {
            isActive = Convert.ToBoolean(reader["isActive"]);
            lastUpdateDate = Convert.ToDateTime(reader["lastUpdateDate"]);
        }

        public bool isActive { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}