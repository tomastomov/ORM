using ORM.Contracts;
using ORM.Contracts.Factories;
using ORM.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Factories
{
    internal class EntityRelationshipFactory : IEntityRelationshipFactory
    {
        public IEntityRelationship Create<TEntity, TRelatedEntity>(RelationshipType relationshipType)
        {
            return new EntityRelationship(typeof(TEntity), typeof(TRelatedEntity), relationshipType);
        }
    }
}
