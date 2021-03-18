using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Extensions;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityDataBuilder<TEntity> : IEntityDataBuilder<TEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        private readonly IEntityRelationship relationship_;
        public EntityDataBuilder(IModelDataStorage<Type> storage, IEntityRelationship relationship)
        {
            storage_ = storage;
            relationship_ = relationship;
        }
        public IEntityDataBuilder<TEntity> HasForeignKey<TKey>(Expression<Func<TEntity, TKey>> dataSelector)
        {
            var propertyInfo = dataSelector.GetPropertyInfo();

            var entityType = typeof(TEntity);

            storage_.Get(entityType)?.Keys.Add(new ForeignKey(propertyInfo.PropertyType, propertyInfo, relationship_));

            return this;
        }
    }
}
