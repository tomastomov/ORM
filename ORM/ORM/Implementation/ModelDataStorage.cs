using ORM.Contracts;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM.Implementation
{
    internal class ModelDataStorage<TEntity> : IModelDataStorage<TEntity>
    {
        private readonly IDictionary<TEntity, ModelData> models_ = new Dictionary<TEntity, ModelData>();
        public void Add(TEntity key, ModelData modelData)
        {
            if (!models_.ContainsKey(key))
            {
                models_.Add(key, new ModelData());
            }
        }

        public ModelData Get(TEntity key)
        {
            if (models_.TryGetValue(key, out var modelData))
            {
                return modelData;
            }

            throw new ArgumentException($"No such key: {key} found");
        }

        public IKey GetPrimaryKey(TEntity entity)
        {
            var entityModel = Get(entity);

            return entityModel.Keys.FirstOrDefault(k => k.GetType() == typeof(PrimaryKey));
        }

        public bool TryGetValue(TEntity key, out ModelData modelData)
            => models_.TryGetValue(key, out modelData);
    }
}
