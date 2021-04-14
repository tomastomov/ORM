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
        private readonly IDictionary<(Type, string), object> trackedEntities_;
        private readonly IModelDataStorage<Type> modelDataStorage_;
        public StateManager(IModelDataStorage<Type> modelDataStorage)
        {
            trackedEntities_ = new Dictionary<(Type, string), object>();
            modelDataStorage_ = modelDataStorage;
        }

        public void AddEntity<TEntity>(TEntity entity)
        {
            var entityType = entity.GetType();

            var primaryKey = GetPrimaryKeyValue(entity);

            if (trackedEntities_.ContainsKey((entityType, primaryKey)))
            {
                trackedEntities_.Add((entityType, primaryKey), entity);
            }
        }

        public TEntity GetOrAddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            var entityType = entity.GetType();

            var primaryKey = GetPrimaryKeyValue(entity);

            if (!trackedEntities_.TryGetValue((entityType, primaryKey), out var trackedEntity))
            {
                trackedEntities_.Add((entityType, primaryKey), entity);
                trackedEntity = entity;
            }

            return (TEntity)trackedEntity;
        }

        private string GetPrimaryKeyValue<TEntity>(TEntity entity)
            => modelDataStorage_.GetPrimaryKey(entity.GetType())?.Property.GetValue(entity)?.ToString();
    }
}
