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
        private readonly IStateManager stateManager_;
        public InternalDatabaseTable(IDatabase database, IStateManager stateManager, IExpressionVisitor visitor, string tableName, Expression expression = null)
        {
            database_ = database;
            stateManager_ = stateManager;
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
            throw new NotSupportedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) 
            => new InternalDatabaseTable<TElement>(database_, stateManager_, visitor_, TableName, expression);

        public object Execute(Expression expression)
            => Provider.Execute<object>(expression);

        public override void Add(TEntity entity)
        {
            stateManager_.AddEntity(entity);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var query = visitor_.Visit(expression);

            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine("USE MyORMDB");
            var selectCount = visitor_.FirstOrDefaultClause ?? "*";
            queryBuilder.AppendLine($"SELECT {selectCount} FROM {TableName}");
            queryBuilder.Append(query);

            var command = database_.CreateCommand(c => c.WithCommandText(queryBuilder.ToString()));
            var result = database_.ExecuteCommand<TResult>(command);

            return result;
        }

        public override IEnumerator<TEntity> GetEnumerator() 
            => (Provider.Execute<IEnumerable<TEntity>>(Expression)).GetEnumerator();
    }
}
