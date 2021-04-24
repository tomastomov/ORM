using ORM.Contracts.Builders;
using ORM.Extensions;
using ORM.Helpers;
using ORM.Implementation;
using ORM.Implementation.Builders;
using ORM.Implementation.Constraints;
using ORM.Implementation.Keys;
using ORM.Implementation.Translators;
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
        private readonly IQueryTranslator<IUpdatedEntity, string> updateQueryTranslator_;
        private readonly IQueryTranslator<EntityData, string> dbQueryTranslator_;
        private readonly IDatabase database_;
        private readonly IModelDataStorage<Type> modelDataStorage_;
        private readonly IConstraintTranslator constraintTranslator_;
        private IDictionary<Type, string> dbTableToPropertyName_;
        private IStateManager stateManager_;
        private ITableNameProvider tableNameProvider_;
        private IChangeDetector changeDetector_;
        public DatabaseContext(DatabaseContextOptions options)
        {
            options_ = options;
            modelDataStorage_ = new ModelDataStorage<Type>();
            dbQueryTranslator_ = new DatabaseCreationQueryTranslator(modelDataStorage_);
            constraintTranslator_ = new ConstraintTranslator(options);
            dbTableToPropertyName_ = new Dictionary<Type, string>();
            stateManager_ = new StateManager(modelDataStorage_);
            database_ = new SqlDatabase(new SqlEntityDeserializer(modelDataStorage_, stateManager_), options_.ConnectionString);
            tableNameProvider_ = new TableNameProvider();
            changeDetector_ = new ChangeDetector(tableNameProvider_, modelDataStorage_);
            updateQueryTranslator_ = new UpdateQueryTranslator(new WhereQueryTranslator());
        }

        public virtual void OnModelCreating(IModelBuilder builder)
        {

        }

        public void CreateDatabase()
        {
            OnModelCreating(new ModelBuilder(modelDataStorage_));

            if (!CheckDatabaseExists(options_.ConnectionString, options_.DatabaseName))
            {
                var result = database_.ExecuteCommand(database_.CreateCommand(b => b.WithConnectionString(options_.ConnectionString).WithCommandText($"CREATE DATABASE {options_.DatabaseName}")));
            }

            var entities = this.GetType()
            .GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DatabaseTable<>))
            .Select(p => new EntityData
            {
                PropertyName = p.Name,
                EntityType = p.PropertyType.GetGenericArguments()[0]
            });

            dbTableToPropertyName_ = entities.ToDictionary(k => k.EntityType, k => k.PropertyName);

            entities.Each(entity =>
            {
                ProcessDatabaseTableCreation(entity);
            })
            .Each(entity =>
            {
                modelDataStorage_.Get(entity.EntityType).Keys
                .Each(key =>
                {
                    ProcessConstraints(entity, key);
                });
            });

            this.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DatabaseTable<>))
                .Select(p => new
                {
                    Property = p,
                    GenericArgument = p.PropertyType.GetGenericArguments()[0]
                })
                .Each(e =>
                {
                    e.Property.SetValue(this, Activator.CreateInstance(typeof(InternalDatabaseTable<>).MakeGenericType(e.GenericArgument), new object[5] { database_, stateManager_, new GenericExpressionVisitor(), e.Property.Name, null }));
                    tableNameProvider_.AddTableName(e.GenericArgument, e.Property.Name);
                });
        }

        private void ProcessDatabaseTableCreation(EntityData entity)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"{SQLCommandConstants.USE} {options_.DatabaseName}");
            queryBuilder.Append(dbQueryTranslator_.Translate(entity));
            var executionResult = database_.ExecuteCommand(database_.CreateCommand(b => b.WithCommandText(queryBuilder.ToString()).WithConnectionString(options_.ConnectionString)));
        }

        private void ProcessConstraints(EntityData entity, IKey key)
        {
            IConstraint constraint = null;

            if (key is ForeignKey foreignKey)
            {
                var referencedTableName = dbTableToPropertyName_[foreignKey.Relationship.RelatedEntity];
                constraint = new ForeignKeyConstraint(key, entity.PropertyName, referencedTableName);
            }
            else if (key is PrimaryKey primaryKey)
            {
                constraint = new PrimaryKeyConstraint(key, entity.PropertyName);
            }

            var query = constraintTranslator_.Translate(constraint);
            var executionResult = database_.ExecuteCommand(database_.CreateCommand(b => b.WithCommandText(query).WithConnectionString(options_.ConnectionString)));
        }

        private bool CheckDatabaseExists(string connectionString, string databaseName)
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

        public virtual bool SaveChanges()
        {
            try
            {
                var updatedEntries = changeDetector_.DetectChanges(stateManager_.GetTrackedEntities<object>())
                    .Where(update => update.EntityUpdates.Count() > 0)
                    .Each(update =>
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine($"USE {options_.DatabaseName}");
                            sb.AppendLine(updateQueryTranslator_.Translate(update));
                            var command = database_.CreateCommand(builder => builder.WithCommandText(sb.ToString()));
                            database_.ExecuteCommand(command);
                        });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
