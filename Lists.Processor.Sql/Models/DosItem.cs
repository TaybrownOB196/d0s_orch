using System.Data.Common;
using System;

namespace Lists.Processor.Sql.Models
{
    public class DosItem : DbModel
    {
        public DosItem(DbDataReader reader)
            : base(reader)
        {
            name = Convert.ToString(reader["name"]);
            description = Convert.ToString(reader["description"]);
        }

        public string name { get; set; }
        public string description { get; set; }
    }
}