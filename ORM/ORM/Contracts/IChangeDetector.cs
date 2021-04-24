using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IChangeDetector
    {
        IEnumerable<IUpdatedEntity> DetectChanges(IEnumerable<ITrackedInternalEntity<object>> entities);
    }
}
