using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityDataBuilder<TEntity>
    {
        IEntityDataBuilder<TEntity> HasForeignKey<TKey>(Expression<Func<TEntity, TKey>> dataSelector);
    }
}
