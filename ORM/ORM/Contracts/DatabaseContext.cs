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
        private readonly IDatabase database_ = new Database();
        public DatabaseContext(DatabaseContextOptions options)
        {
            options_ = options;
        }

        public void CreateDatabase()
        {
            if (!CheckDatabaseExists(options_.ConnectionString, options_.DatabaseName))
            {
                var result = database_.ExecuteCommand(database_.CreateCommand(b => b.WithConnectionString(options_.ConnectionString).WithCommandText($"CREATE DATABASE {options_.DatabaseName}")));
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

            var executionResult = database_.ExecuteCommand(database_.CreateCommand(b => b.WithCommandText(query).WithConnectionString(options_.ConnectionString)));
        }

        public static bool CheckDatabaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new System.Data.SqlClient.SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
    }
}
