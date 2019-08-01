using System;
using Lists.Processor.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Lists.Processor.Sql.Models;
using Lists.Processor.Sql.Operations;

namespace Lists.Processor.Sql
{
    public class Database : IDatabase 
    {
        private readonly string _connString;
        private readonly ILogger _logger;

        public Database(IOptionsMonitor<SqlOptions> sqlOption, ILogger logger)
        {
            _connString = sqlOption.CurrentValue.ConnectionString;
            _logger = logger;
        }

        public T ExecuteQuery<T>(IDatabaseRetrievalOperation<T> operation) where T : DbModel
        {
            var cmd = new MySqlCommand(operation.QueryString());
            try 
            {
                using (MySqlConnection connection = new MySqlConnection(_connString))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            return operation.ResultOrDefault(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return default;
        }
    }
}
