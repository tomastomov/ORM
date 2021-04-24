using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IUpdatedEntity : IIdentifiableEntity<object>
    {
        string TableName { get; }

        IEnumerable<IEntityUpdate> EntityUpdates { get; }
    }
}
