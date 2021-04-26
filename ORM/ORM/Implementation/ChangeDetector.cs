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
                  .Where(t => !t.GetGetMethod().IsVirtual)
                  .Where(p => !CompareBoxedValues(p.GetValue(e.Entity), e.Snapshot.GetType().GetProperty(p.Name).GetValue(e.Snapshot)))
                  .Select(p => new EntityUpdate(p.Name, p.GetValue(e.Entity))), CreateIdentifier(e.Entity, modelDataStorage_.GetPrimaryKey(e.Entity.GetType()).Property)));
        }

        private bool CompareBoxedValues(object first, object second)
        {
            if (first.GetType().IsValueType && second.GetType().IsValueType)
            {
                first = Convert.ChangeType(first, first.GetType());
                second = Convert.ChangeType(second, second.GetType());
            }

            return first.Equals(second);
        } 

        private LambdaExpression CreateIdentifier<T>(T instance, PropertyInfo property)
        {
            var param = Expression.Parameter(instance.GetType(), "obj");
            
            var binaryExpr = Expression.MakeBinary(ExpressionType.Equal, Expression.MakeMemberAccess(param, property), Expression.Constant(property.GetValue(instance)));
            var func = typeof(Func<,>).MakeGenericType(instance.GetType(), typeof(bool));
            var expr = Expression.Lambda(func, binaryExpr, param);

            return expr;
        }
    }
}
