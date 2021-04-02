using ORM.Contracts;
using ORM.Helpers;
using ORM.Implementation.SqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    internal class DatabaseCreationQueryTranslator : IQueryTranslator<EntityData ,string>
    {
        private readonly IModelDataStorage<Type> modelDataStorage_;
        private readonly ISqlTypeConverter converter_ = new SqlTypeConverter();
        public DatabaseCreationQueryTranslator(IModelDataStorage<Type> modelDataStorage)
        {
            modelDataStorage_ = modelDataStorage;
        }
        public string Translate(EntityData query)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{SQLCommandConstants.CREATE_TABLE} {query.PropertyName} (");

            var entityProperties = query.EntityType.GetProperties()
                .Where(p => !IsPropertyIgnored(query.EntityType, p))
                .Aggregate(builder, (sb, next) =>
                {
                    var sqlType = converter_.Convert<Type, SqlDataType>(next.PropertyType);
                    var nullConstraint = typeof(Nullable<>).IsAssignableFrom(next.PropertyType) ? string.Empty : SQLCommandConstants.NOT_NULL;
                    sb.AppendLine($"{next.Name} {sqlType.SqlType} {nullConstraint},");
                    return sb;
                });

            builder.AppendLine(");");

            return builder.ToString();
        }

        private bool IsPropertyIgnored(Type entityType, PropertyInfo property)
        {
            var isIgnored = modelDataStorage_.Get(entityType)?.EntityPropertiesData.Any(p => p.Property == property && p.IsIgnored);

            if (isIgnored == null)
            {
                return false;
            }

            return isIgnored.Value;
        }
    }
}
