using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface ITrackedInternalEntity<TEntity> : IInternalEntity<TEntity>
    {
        TEntity Snapshot { get; }
    }
}
