using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Contracts.Builders
{
    public interface IEntityPropertyBuilder<TEntity>
    {
        IEntityPropertyOptionsBuilder<TEntity> HasProperty<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector);
    }
}
