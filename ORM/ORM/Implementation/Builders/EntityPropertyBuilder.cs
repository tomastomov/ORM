using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityPropertyBuilder<TEntity> : IEntityPropertyBuilder<TEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        public EntityPropertyBuilder(IModelDataStorage<Type> storage)
        {
            storage_ = storage;
        }

        public IEntityPropertyOptionsBuilder<TEntity> HasProperty<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            var key = typeof(TEntity);
            var propertyInfo = propertySelector.GetPropertyInfo();

            storage_.Get(typeof(TEntity))?.EntityPropertiesData.Add(new EntityPropertyData() { Property = propertyInfo });

            return new EntityPropertyOptionsBuilder<TEntity>(storage_, propertyInfo);
        }
    }
}
