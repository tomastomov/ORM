using ORM.Implementation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IEntityRelationship
    {
        RelationshipType RelationshipType { get; }

        Type Entity { get; }

        Type RelatedEntity { get; }
    }
}
