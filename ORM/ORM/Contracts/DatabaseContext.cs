using ORM.Implementation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM.Contracts
{
    public abstract class DatabaseContext : IDatabaseContext
    {
        private DatabaseContextOptions options_;
        private readonly IQueryTranslator<EntityData, string> dbQueryTranslator_ = new DatabaseCreationQueryTranslator();
        public DatabaseContext(DatabaseContextOptions options)
        {
            options_ = options;
        }

        public void CreateDatabase()
        {
            using var connection = new SqlConnection(options_.ConnectionString);
            if (!CheckDatabaseExists(options_.ConnectionString, options_.DatabaseName))
            {
                var dbCreationCommand = connection.CreateCommand();
                dbCreationCommand.CommandText = $"CREATE DATABASE {options_.DatabaseName}";
                connection.Open();
                var result = dbCreationCommand.ExecuteNonQuery();
                connection.Close();

            }
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"USE {options_.DatabaseName}");
            var query =
                this.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DatabaseTable<>))
                .Select(p => new EntityData
                {
                    PropertyName = p.Name,
                    EntityType = p.PropertyType.GetGenericArguments()[0]
                }).Aggregate(queryBuilder, (sb, next) =>
                {
                    sb.AppendLine(dbQueryTranslator_.Translate(next));
                    return sb;
                }).ToString();

            connection.Open();
            var command = new SqlCommand(query, connection);
            var tableCreationResult = command.ExecuteNonQuery();
            connection.Close();
        }

        public static bool CheckDatabaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
    }
}
