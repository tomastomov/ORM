using ORM.Contracts;
using System;
using System.Collections.Generic;
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
            if (!models_.TryGetValue(key, out var modelData))
            {
                return modelData;
            }

            throw new ArgumentException($"No such key: {key} found");
        }
    }
}
