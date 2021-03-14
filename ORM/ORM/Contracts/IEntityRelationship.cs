using ORM.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IEntityRelationship<TEntity, TRelatedEntity>
    {
        RelationshipType RelationshipType { get; }  
    }
}
