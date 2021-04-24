using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class InternalEntity<TEntity> : ITrackedInternalEntity<TEntity>
    {
        public InternalEntity(TEntity entity)
        {
            Entity = entity;
            Snapshot = (TEntity)ObjectCloner.Clone(entity);
        }

        public TEntity Entity { get; private set; }

        public TEntity Snapshot { get; private set; }
    }
}
