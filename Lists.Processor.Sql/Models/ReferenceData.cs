using System;
using System.Data.Common;

namespace Lists.Processor.Sql.Models
{
    public class ReferenceData : DbModel
    {
        public ReferenceData(DbDataReader reader)
            : base(reader)
        {
            key = Convert.ToString(reader["keyRef"]);
            value = Convert.ToString(reader["value"]);
        }

        public string key { get; set; }
        public string value { get; set; }
    }
}