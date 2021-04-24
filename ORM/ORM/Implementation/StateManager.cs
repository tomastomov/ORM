using ORM.Contracts;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM.Implementation
{
    internal class StateManager : IStateManager
    {
        private readonly IDictionary<(Type, string), IInternalEntity<object>> trackedEntities_;
        private readonly IModelDataStorage<Type> modelDataStorage_;
        private readonly Queue<IInternalEntity<object>> entitiesToBeAdded_;
        public StateManager(IModelDataStorage<Type> modelDataStorage)
        {
            trackedEntities_ = new Dictionary<(Type, string), IInternalEntity<object>>();
            modelDataStorage_ = modelDataStorage;
            entitiesToBeAdded_ = new Queue<IInternalEntity<object>>();
        }

        public void AddEntity<TEntity>(TEntity entity)
        {
            entitiesToBeAdded_.Enqueue((IInternalEntity<object>)new InternalEntity<TEntity>(entity));
        }

        public TEntity GetOrAddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            var entityType = entity.GetType();

            var primaryKey = GetPrimaryKeyValue(entity);

            if (!trackedEntities_.TryGetValue((entityType, primaryKey), out var trackedEntity))
            {
                trackedEntity = (IInternalEntity<object>)new InternalEntity<TEntity>(entity);
                trackedEntities_.Add((entityType, primaryKey), trackedEntity);
            }

            return (TEntity)trackedEntity;
        }

        public IEnumerable<ITrackedInternalEntity<TEntity>> GetTrackedEntities<TEntity>()
            => (IEnumerable<ITrackedInternalEntity<TEntity>>)trackedEntities_.Values.ToList();

        private string GetPrimaryKeyValue<TEntity>(TEntity entity)
            => modelDataStorage_.GetPrimaryKey(entity.GetType())?.Property.GetValue(entity)?.ToString();
    }
}
