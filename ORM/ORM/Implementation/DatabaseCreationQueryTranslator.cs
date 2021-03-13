using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    internal class DatabaseCreationQueryTranslator : IQueryTranslator<EntityData ,string>
    {
        private readonly ISqlTypeConverter converter_ = new SqlTypeConverter();
        public string Translate(EntityData query)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"CREATE TABLE {query.PropertyName} (");

            var entityProperties = query.EntityType.GetProperties()
                .Aggregate(builder, (sb, next) =>
                {
                    var sqlType = converter_.Convert<Type, SqlDataType>(next.PropertyType);
                    sb.AppendLine($"{next.Name} {sqlType.SqlType}, ");
                    return sb;
                });

            builder.AppendLine(");");

            return builder.ToString();
        }
    }
}
