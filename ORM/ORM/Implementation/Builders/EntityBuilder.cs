using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ORM.Extensions;
using ORM.Implementation.Enums;

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

            var entityType = GetEntityType<TEntity>();

            var modelData = storage_.Get(entityType);

            var key = new PrimaryKey(propertyInfo.PropertyType, propertyInfo);

            modelData.Keys.Add(key);

            return new EntityBuilder<TEntity>(storage_);
        }

        public IEntityNavigationBuilder<TRelatedEntity, TEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector)
        {
            CreateRelationship<TRelatedEntity>(RelationshipType.Many);

            return new EntityNavigationBuilder<TRelatedEntity, TEntity>(storage_);
        }

        public IEntityNavigationBuilder<TRelatedEntity, TEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
        {
            CreateRelationship<TRelatedEntity>(RelationshipType.One);

            return new EntityNavigationBuilder<TRelatedEntity, TEntity>(storage_);
        }

        private void CreateRelationship<TRelatedEntity>(RelationshipType relationshipType)
        {
            var relatedEntityType = GetEntityType<TRelatedEntity>();
            var entityType = GetEntityType<TEntity>();
            var relationship = new EntityRelationship(typeof(TEntity), typeof(TRelatedEntity), relationshipType);
            var modelData = GetModelData(entityType);
            modelData.Relationships.Add(relationship);
        }

        private ModelData GetModelData(Type key)
            => storage_.Get(key);

        private Type GetEntityType<T>()
            => typeof(T);
    }
}
