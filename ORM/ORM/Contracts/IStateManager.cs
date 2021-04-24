using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IStateManager
    {
        void AddEntity<TEntity>(TEntity entity);

        TEntity GetOrAddEntity<TEntity>(TEntity entity) where TEntity : class;

        IEnumerable<ITrackedInternalEntity<TEntity>> GetTrackedEntities<TEntity>();
    }
}
