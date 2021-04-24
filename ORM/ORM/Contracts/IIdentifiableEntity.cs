using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    internal interface IIdentifiableEntity
    {
        LambdaExpression DbIdentifier { get; }
    }
}
