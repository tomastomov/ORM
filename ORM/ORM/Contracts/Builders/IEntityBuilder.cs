using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityBuilder<TEntity> : IEntityRelationshipBuilder<TEntity>, IEntityPropertyBuilder<TEntity>
    {
        IEntityBuilder<TEntity> HasKey<TKey>(Expression<Func<TEntity, TKey>> propertySelector);
    }
}
