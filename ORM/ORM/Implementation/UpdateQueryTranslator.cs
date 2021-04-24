using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class UpdateQueryTranslator : IQueryTranslator<IUpdatedEntity, string>
    {
        private readonly IQueryTranslator<LambdaExpression, string> whereTranslator_;
        public UpdateQueryTranslator(IQueryTranslator<LambdaExpression, string> whereTranslator)
        {
            whereTranslator_ = whereTranslator;
        }
        public string Translate(IUpdatedEntity query)
        {
            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine($"UPDATE {query.TableName}");
            queryBuilder.AppendLine("SET ");

            queryBuilder.AppendLine(query.EntityUpdates.Aggregate(new StringBuilder(), (sb, curr) =>
            {
                sb.AppendLine($"{curr.ColumnName} = {curr.UpdatedValue}, ");

                return sb;
            }, sb => sb.ToString().TrimEnd(' ').TrimEnd(',')));

            queryBuilder.AppendLine(whereTranslator_.Translate(query.DbIdentifier.Body as LambdaExpression));

            return queryBuilder.ToString();
        }
    }
}
