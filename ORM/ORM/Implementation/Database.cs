using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ORM.Implementation
{
    internal class Database : IDatabase
    {
        public ICommand CreateCommand(Action<ICommandOptionsBuilder> builder)
        {
            var commandBuilder = new SqlCommandOptionsBuilder();
            builder?.Invoke(commandBuilder);

            return commandBuilder.Build(); 
        }

        public ICommandExecutionResult ExecuteCommand(ICommand command)
        {
            using var connection = new SqlConnection(command.ConnectionString);
            var sqlCommand = new SqlCommand(command.CommandText, connection);
            connection.Open();
            var result = sqlCommand.ExecuteNonQuery();

            return new CommandExecutionResult(result);
        }
    }
}
