﻿using ORM.Contracts;
using ORM.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityNavigationBuilder<TEntity, TRelatedEntity> : IEntityNavigationBuilder<TEntity, TRelatedEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        public EntityNavigationBuilder(IModelDataStorage<Type> storage)
        {
            storage_ = storage;
        }
        public IEntityDataBuilder<TRelatedEntity> WithMany(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector)
        {
            throw new NotImplementedException();
        }

        public IEntityDataBuilder<TEntity> WithOne(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
        {
            throw new NotImplementedException();
        }
    }
}
