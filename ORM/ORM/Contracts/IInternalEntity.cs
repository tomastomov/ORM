using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IInternalEntity<TEntity>
    {
        TEntity Entity { get; }
    }
}
