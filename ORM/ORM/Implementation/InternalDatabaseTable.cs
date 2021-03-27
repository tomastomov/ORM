using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class InternalDatabaseTable<TEntity> : DatabaseTable<TEntity>, IQueryProvider
    {
        private readonly IExpressionVisitor visitor_;
        private readonly IDatabase database_;
        public InternalDatabaseTable(IDatabase database, IExpressionVisitor visitor, string tableName, Expression expression = null)
        {
            database_ = database;
            TableName = tableName;
            visitor_ = visitor;
            Expression = expression ?? Expression.Constant(this);
        }

        public string TableName { get; private set; }

        public override Expression Expression { get; }

        public override Type ElementType => typeof(TEntity);

        public override IQueryProvider Provider => this;

        public IQueryable CreateQuery(Expression expression)
        {
            
            return null;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new InternalDatabaseTable<TElement>(database_, visitor_, TableName, expression);
        }

        public object Execute(Expression expression)
            => Provider.Execute<object>(expression);

        public TResult Execute<TResult>(Expression expression)
        {
            var query = visitor_.Visit(expression);

            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine($"SELECT * FROM {TableName}");
            queryBuilder.Append(query);

            var command = database_.CreateCommand(c => c.WithCommandText(query));
            var result = database_.ExecuteCommand(command);

            return default;
        }
        public override IEnumerator<TEntity> GetEnumerator() => ((IEnumerable<TEntity>)Provider.Execute(Expression)).GetEnumerator();
    }
}
