using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlDatabase : IDatabase
    {
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
                using var connection = new SqlConnection(command.ConnectionString);
                var sqlCommand = new SqlCommand(command.CommandText, connection);
                connection.Open();
                var result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            return new CommandExecutionResult(true);
        }
    }
}
