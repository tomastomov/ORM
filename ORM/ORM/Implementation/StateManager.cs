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
        private readonly IDictionary<string, object> trackedEntities_;
        private readonly IModelDataStorage<Type> modelDataStorage_;
        public StateManager(IModelDataStorage<Type> modelDataStorage)
        {
            trackedEntities_ = new Dictionary<string, object>();
            modelDataStorage_ = modelDataStorage;
        }

        public void AddEntity<TEntity>(TEntity entity)
        {
            var primaryKey = GetPrimaryKeyValue(entity);
                //if it is null generate one
            if (!trackedEntities_.ContainsKey(primaryKey))
            {
                trackedEntities_.Add(primaryKey, entity);
            }
        }

        public TEntity GetOrAddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            var primaryKey = GetPrimaryKeyValue(entity);

            if (!trackedEntities_.TryGetValue(primaryKey, out var trackedEntity))
            {
                trackedEntities_.Add(primaryKey, entity);
                trackedEntity = entity;
            }

            return (TEntity)trackedEntity;
        }

        private string GetPrimaryKeyValue<TEntity>(TEntity entity)
        {
            var entityModel = modelDataStorage_.Get(typeof(TEntity));
            var primaryKey = entityModel.Keys.FirstOrDefault(k => k.GetType() == typeof(PrimaryKey)).Property.GetValue(entity)?.ToString();

            return primaryKey;
        }
    }
}
