using ORM.Contracts;
using ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    internal class ChangeDetector : IChangeDetector
    {
        private readonly ITableNameProvider tableNameProvider_;
        public ChangeDetector(ITableNameProvider tableNameProvider)
        {
            tableNameProvider_ = tableNameProvider;
        }
        public IEnumerable<IUpdatedEntity> DetectChanges(IEnumerable<ITrackedInternalEntity<object>> entities)
            => entities.Select(e => new UpdatedEntity(tableNameProvider_.GetTableName(e.Entity.GetType()), e.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => !typeof(IEnumerable<>).IsAssignableFrom(t.PropertyType))
                .Where(p => p.GetValue(e.Entity) != e.Snapshot.GetType().GetProperty(p.Name).GetValue(e.Entity))
                .Select(p => new EntityUpdate(p.Name, p.GetValue(e.Entity))), null));
    }
}
