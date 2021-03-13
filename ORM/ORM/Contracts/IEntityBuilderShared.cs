using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts
{
    public interface IEntityBuilderShared<TEntity>
    {
        IEntityBuilder<TKey> WithMany<TKey>(Expression<Func<TEntity, IEnumerable<TKey>>> propertySelector);

        IEntityBuilder<TKey> WithOne<TKey>(Expression<Func<TEntity, TKey>> propertySelector);
    }
}
