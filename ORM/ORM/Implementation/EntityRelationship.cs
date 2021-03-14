using ORM.Contracts;
using ORM.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class EntityRelationship : IEntityRelationship
    {
        public EntityRelationship(Type entity, Type relatedEntity, RelationshipType relationshipType)
        {
            RelationshipType = relationshipType;
            Entity = entity;
            RelatedEntity = relatedEntity;
        }
        public RelationshipType RelationshipType { get; private set; }

        public Type Entity { get; private set; }

        public Type RelatedEntity { get; private set; }
    }
}
