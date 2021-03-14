﻿using ORM.Contracts;
using ORM.Contracts.Builders;
using ORM.Contracts.Factories;
using ORM.Implementation.Enums;
using ORM.Implementation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Builders
{
    internal class EntityNavigationBuilder<TEntity, TRelatedEntity> : IEntityNavigationBuilder<TEntity, TRelatedEntity>
    {
        private readonly IModelDataStorage<Type> storage_;
        private readonly IEntityRelationshipFactory entityRelationshipFactory_;
        public EntityNavigationBuilder(IModelDataStorage<Type> storage)
        {
            storage_ = storage;
            entityRelationshipFactory_ = new EntityRelationshipFactory();
        }
        public IEntityDataBuilder<TRelatedEntity> WithMany(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> entitySelector)
        {
            return BuildRelationship<TRelatedEntity>(RelationshipType.Many);
        }

        public IEntityDataBuilder<TEntity> WithOne(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
        {
            return BuildRelationship<TEntity>(RelationshipType.One);
        }
        
        private IEntityRelationship GetPreviousRelationship()
        {
            var entityType = typeof(TEntity);
            var relatedType = typeof(TRelatedEntity);

            return storage_.Get(entityType)?.Relationships.FirstOrDefault(r => r.Entity == entityType && r.RelatedEntity == relatedType);
        }

        private IEntityDataBuilder<T> BuildRelationship<T>(RelationshipType relationshipType)
        {
            var pendingRelationship = GetPreviousRelationship();

            var relationship = entityRelationshipFactory_.Create<TEntity, TRelatedEntity>(relationshipType | pendingRelationship.RelationshipType);

            return new EntityDataBuilder<T>(storage_);
        }
    }
}
