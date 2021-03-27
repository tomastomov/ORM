using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlDatabase : IDatabase
    {
        private string defaultConnectionString_;
        public SqlDatabase(string connectionString)
        {
            defaultConnectionString_ = connectionString;
        }
        public ICommand CreateCommand(Action<ICommandOptionsBuilder> builder)
        {
            var commandBuilder = new SqlCommandOptionsBuilder();
            builder?.Invoke(commandBuilder);

            return commandBuilder.Build(); 
        }

        public ICommandExecutionResult ExecuteCommand(ICommand command)
        {
            try
            {
                using var connection = new SqlConnection(command.ConnectionString ?? defaultConnectionString_);
                var sqlCommand = new SqlCommand(command.CommandText, connection);
                connection.Open();
                var result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
           
            return new CommandExecutionResult(true);
        }
    }
}
