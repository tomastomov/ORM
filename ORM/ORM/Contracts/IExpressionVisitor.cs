using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public interface IExpressionVisitor
    {
        Expression Visit(Expression expression);
    }
}
