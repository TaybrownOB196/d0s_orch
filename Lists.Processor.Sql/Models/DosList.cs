using System.Collections.Generic;
using System;
using System.Data.Common;

namespace Lists.Processor.Sql.Models
{
    public class DosList : DbModel
    {
        public DosList(DbDataReader reader)
            : base(reader)
        {
            name = Convert.ToString(reader["name"]);
            description = Convert.ToString(reader["description"]);
        }

        public string name { get; set; }
        public string description { get; set; }
        public IEnumerable<DosItem> items { get; set; } 
    }
}