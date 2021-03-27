using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ORM.Implementation
{
    internal class InternalDatabaseTable<TEntity> : DatabaseTable<TEntity>, IQueryProvider
    {
        private readonly IExpressionVisitor visitor = new GenericExpressionVisitor();
        public override Expression Expression => Expression.Constant(this);

        public override Type ElementType => typeof(TEntity);

        public override IQueryProvider Provider => this;

        public IQueryable CreateQuery(Expression expression)
        {
            
            return null;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            visitor.Visit(expression);

            return null;
        }

        public object Execute(Expression expression)
            => Provider.Execute<object>(expression);

        public TResult Execute<TResult>(Expression expression)
        {
            visitor.Visit(expression);

            return default;
        }
        public override IEnumerator<TEntity> GetEnumerator() => ((IEnumerable<TEntity>)Provider.Execute(Expression.Constant(this))).GetEnumerator();
    }
}
