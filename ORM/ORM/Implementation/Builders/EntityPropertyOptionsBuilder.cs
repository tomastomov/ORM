using ORM.Contracts;
using ORM.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityPropertyOptionsBuilder<TEntity> : IEntityPropertyOptionsBuilder<TEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        private readonly PropertyInfo propertyInfo_;
        public EntityPropertyOptionsBuilder(IModelDataStorage<Type> storage, PropertyInfo property)
        {
            storage_ = storage;
            propertyInfo_ = property;
        }
        public IEntityPropertyOptionsBuilder<TEntity> Ignore()
        {
            var property = storage_?.Get(typeof(TEntity))?.EntityPropertiesData.FirstOrDefault(p => p.Property.PropertyType == propertyInfo_.PropertyType);

            if (property != null)
            {
                property.IsIgnored = true;
            }

            return this;
        }
    }
}
