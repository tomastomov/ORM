using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public interface IExpressionVisitor
    {
        string Visit(Expression expression);

        public string WhereClause { get; }
        public string SelectClause { get; }
    }
}
