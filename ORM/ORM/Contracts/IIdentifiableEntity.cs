using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    internal interface IIdentifiableEntity<T>
    {
        Expression<Func<T, bool>> DbIdentifier { get; }
    }
}
