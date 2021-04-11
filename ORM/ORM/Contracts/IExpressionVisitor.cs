using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public interface IExpressionVisitor
    {
        string Visit(Expression expression);

        string WhereClause { get; }
        string SelectClause { get; }
        string FirstOrDefaultClause { get; }
    }
}
