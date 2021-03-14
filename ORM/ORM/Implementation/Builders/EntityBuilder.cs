using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ORM.Extensions;
using ORM.Implementation.Enums;
using ORM.Contracts.Factories;
using ORM.Implementation.Factories;

namespace ORM.Implementation.Builders
{
    internal class EntityBuilder<TEntity> : IEntityBuilder<TEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        private readonly IEntityRelationshipFactory entityRelationshipFactory_;
        private readonly IEntityPropertyBuilder<TEntity> entityPropertyBuilder_;
        public EntityBuilder(IModelDataStorage<Type> storage)
        {
            storage_ = storage;
            entityRelationshipFactory_ = new EntityRelationshipFactory();
            entityPropertyBuilder_ = new EntityPropertyBuilder<TEntity>(storage_);
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
            var relationship = entityRelationshipFactory_.Create<TEntity, TRelatedEntity>(RelationshipType.Many);

            GetModelData<TEntity>()?.Relationships.Add(relationship);

            return new EntityNavigationBuilder<TRelatedEntity, TEntity>(storage_);
        }

        public IEntityNavigationBuilder<TRelatedEntity, TEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
        {
            var relationship = entityRelationshipFactory_.Create<TEntity, TRelatedEntity>(RelationshipType.One);

            GetModelData<TEntity>()?.Relationships.Add(relationship);

            return new EntityNavigationBuilder<TRelatedEntity, TEntity>(storage_);
        }

        private ModelData GetModelData<T>()
            => storage_.Get(typeof(T));

        private Type GetEntityType<T>()
            => typeof(T);

        public IEntityPropertyOptionsBuilder<TEntity> HasProperty<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            return entityPropertyBuilder_.HasProperty<TProperty>(propertySelector);
        }
    }
}
