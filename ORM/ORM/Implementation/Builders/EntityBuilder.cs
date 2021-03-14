using ORM.Contracts;
using ORM.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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
            var entityType = GetEntityType();
            var modelData = storage_.Get(entityType);

            var memberExpression = propertySelector.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid property selector");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

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
