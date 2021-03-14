using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityNavigationBuilder<TEntity, TRelatedEntity>
    {
        IEntityDataBuilder<TRelatedEntity> WithMany(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector);
        IEntityDataBuilder<TEntity> WithOne(Expression<Func<TEntity, TRelatedEntity>> entitySelector);
    }
}
