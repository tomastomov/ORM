using ORM.Contracts;
using ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    internal class ChangeDetector : IChangeDetector
    {
        private readonly ITableNameProvider tableNameProvider_;
        private readonly IModelDataStorage<Type> modelDataStorage_;
        public ChangeDetector(ITableNameProvider tableNameProvider, IModelDataStorage<Type> modelDataStorage)
        {
            tableNameProvider_ = tableNameProvider;
            modelDataStorage_ = modelDataStorage;
        }
        public IEnumerable<IUpdatedEntity> DetectChanges(IEnumerable<ITrackedInternalEntity<object>> entities)
        {
            return entities.Select(e => new UpdatedEntity(tableNameProvider_.GetTableName(e.Entity.GetType()), e.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                  .Where(t => !typeof(IEnumerable<>).IsAssignableFrom(t.PropertyType))
                  .Where(p => p.GetValue(e.Entity) != e.Snapshot.GetType().GetProperty(p.Name).GetValue(e.Entity))
                  .Select(p => new EntityUpdate(p.Name, p.GetValue(e.Entity))), CreateIdentifier(e.Entity, modelDataStorage_.GetPrimaryKey(e.Entity.GetType()).Property)));
        }

        private Expression<Func<T, bool>> CreateIdentifier<T>(T instance, PropertyInfo property)
        {
            var param = Expression.Parameter(typeof(T), "obj");
            var binaryExpr = Expression.MakeBinary(ExpressionType.Equal, Expression.Constant(property.Name), Expression.Constant(property.GetValue(instance)));
            Expression<Func<T, bool>> expr = Expression.Lambda<Func<T, bool>>(binaryExpr, param);

            return expr;
        }
    }
}
