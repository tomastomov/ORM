using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public interface IEntityBuilder<TEntity>
    {
        IEntityBuilder<TEntity> HasKey<TKey>(Expression<Func<TEntity, TKey>> propertySelector);

        IEntityBuilderShared<TKey> HasOne<TKey>(Expression<Func<TEntity, TKey>> propertySelector);

        IEntityBuilder<TEntity> HasForeignKey<TKey>(Expression<Func<TEntity, TKey>> propertySelector);

        IEntityBuilderShared<TKey> HasMany<TKey>(Expression<Func<TEntity, IEnumerable<TKey>>> propertySelector);
    }
}
