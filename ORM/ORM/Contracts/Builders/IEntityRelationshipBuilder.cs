using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityRelationshipBuilder<TEntity>
    {
        IEntityNavigationBuilder<TRelatedEntity, TEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> entitySelector);
        IEntityNavigationBuilder<TRelatedEntity, TEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector);
    }
}
