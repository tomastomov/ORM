using ORM.Contracts;
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
            => BuildRelationship<TRelatedEntity>(RelationshipType.Many);

        public IEntityDataBuilder<TEntity> WithOne(Expression<Func<TEntity, TRelatedEntity>> entitySelector)
            => BuildRelationship<TEntity>(RelationshipType.One);
        
        private IEntityRelationship GetPreviousRelationship()
        {
            var entityType = typeof(TEntity);
            var relatedType = typeof(TRelatedEntity);

            var entityPreviousRelationship = storage_.Get(entityType)?.Relationships.FirstOrDefault(r => r.Entity == entityType && r.RelatedEntity == relatedType);
            var relatedEntityPreviousRelationship = storage_.Get(relatedType)?.Relationships.FirstOrDefault(r => r.Entity == relatedType && r.RelatedEntity == entityType);

            return entityPreviousRelationship ?? relatedEntityPreviousRelationship;
        }

        private IEntityDataBuilder<T> BuildRelationship<T>(RelationshipType relationshipType)
        {
            var pendingRelationship = GetPreviousRelationship();

            var relationship = entityRelationshipFactory_.Create<TEntity, TRelatedEntity>(BuildRelationshipType(pendingRelationship.RelationshipType, relationshipType));

            storage_.Get(typeof(T))?.Relationships.Add(relationship);

            return new EntityDataBuilder<T>(storage_, relationship);
        }

        private RelationshipType BuildRelationshipType(RelationshipType previousRelationshipType, RelationshipType currentRelationshipType)
        {
            if (previousRelationshipType == RelationshipType.One && currentRelationshipType == RelationshipType.One)
            {
                return RelationshipType.OneToOne;
            }
            else if (previousRelationshipType == RelationshipType.Many && currentRelationshipType == RelationshipType.Many)
            {
                return RelationshipType.ManyToMany;
            }
            else return RelationshipType.OneToMany;
        }
    }
}
