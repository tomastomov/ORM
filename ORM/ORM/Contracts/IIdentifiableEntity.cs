using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    internal interface IIdentifiableEntity
    {
        Expression<Func<object, bool>> DbIdentifier { get; }
    }
}
