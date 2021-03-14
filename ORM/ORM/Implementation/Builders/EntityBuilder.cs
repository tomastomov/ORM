using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ORM.Extensions;

namespace ORM.Implementation.Builders
{
    internal class EntityBuilder<TEntity> : IEntityBuilder<TEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        public EntityBuilder(IModelDataStorage<Type> storage)
        {
            storage_ = storage;
        }
        public IEntityBuilder<TEntity> HasKey<TKey>(Expression<Func<TEntity, TKey>> propertySelector)
        {
            var propertyInfo = propertySelector.GetPropertyInfo();

            var entityType = GetEntityType();
            var modelData = storage_.Get(entityType);

            var key = new PrimaryKey(propertyInfo.PropertyType, propertyInfo);

            modelData.Keys.Add(key);

            return new EntityBuilder<TEntity>(storage_);
        }

        public IEntityNavigationBuilder<TRelatedEntity, TEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector)
        {
            throw new NotImplementedException();
        }

        public IEntityNavigationBuilder<TRelatedEntity, TEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
        {
            throw new NotImplementedException();
        }

        private Type GetEntityType()
            => typeof(TEntity);
    }
}
