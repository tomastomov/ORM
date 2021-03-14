using ORM.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts.Factories
{
    public interface IEntityRelationshipFactory
    {
        IEntityRelationship Create<TEntity, TRelatedEntity>(RelationshipType relationshipType);
    }
}
