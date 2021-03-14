using ORM.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityDataBuilder<TEntity> : IEntityDataBuilder<TEntity>
    {
        public IEntityDataBuilder<TEntity> HasForeignKey<TKey>(Expression<Func<TEntity, TKey>> dataSelector)
        {
            throw new NotImplementedException();
        }
    }
}
