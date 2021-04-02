using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlDatabase : IDatabase
    {
        private readonly string defaultConnectionString_;
        private readonly IEntityDeserializer deserializer_;
        public SqlDatabase(IEntityDeserializer deserializer, string connectionString)
        {
            deserializer_ = deserializer;
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
                var affectedRows = ExecuteCommandImpl(command, (connection, sqlCommand) => sqlCommand.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
            }

            return new CommandExecutionResult(true);
        }

        private TResult ExecuteCommandImpl<TResult>(ICommand command, Func<SqlConnection, SqlCommand, TResult> executeCommand)
        {
            using var connection = GetConnection(command);
            var sqlCommand = GetSqlCommand(command, connection);
            connection.Open();

            return executeCommand(connection, sqlCommand);
        }

        public TResult ExecuteCommand<TResult>(ICommand command)
        {
            try
            {
                return ExecuteCommandImpl(command, (connection, sqlCommand) => ExecuteQuery<TResult>(sqlCommand));
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        private TResult ExecuteQuery<TResult>(SqlCommand command)
        {
            var reader = command.ExecuteReader();

            return deserializer_.Deserialize<TResult>(new SqlDbDataReader(reader));
        }

        private SqlConnection GetConnection(ICommand command)
            => new SqlConnection(command.ConnectionString ?? defaultConnectionString_);

        private SqlCommand GetSqlCommand(ICommand command, SqlConnection connection)
            => new SqlCommand(command.CommandText, connection);
    }
}
