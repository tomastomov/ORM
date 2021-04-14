using ORM.Contracts;
using ORM.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlEntityDeserializer : IEntityDeserializer
    {
        private readonly IModelDataStorage<Type> modelDataStorage_;
        private readonly IStateManager stateManager_;
        public SqlEntityDeserializer(IModelDataStorage<Type> modelDataStorage, IStateManager stateManager)
        {
            modelDataStorage_ = modelDataStorage;
            stateManager_ = stateManager;
        }

        public T Deserialize<T>(IDatabaseDataReader reader)
            => (T)DeserializeImpl(typeof(T), reader, false);

        private object DeserializeImpl(Type type, IDatabaseDataReader reader, bool isEnumerable)
        {
            if (IsEnumerable(type) && type.IsGenericType)
            {
                return DeserializeImpl(type.GetGenericArguments()[0], reader, true);
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.GetGetMethod().IsVirtual);

            var entities = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));

            // TODO add deserializing of structs...

            while (reader.MoveNext())
            {
                var entityInstance = Activator.CreateInstance(type);
                properties.Each(p => p.SetValue(entityInstance, reader.Read(p.PropertyType, p.Name)));

                if (modelDataStorage_.TryGetValue(entityInstance.GetType(), out _))
                {
                    entityInstance = stateManager_.GetOrAddEntity(entityInstance);
                }

                entities.Add(entityInstance);
            }

            return !isEnumerable ? (entities.Count > 0 ? entities[0] : null) : entities;
        }

        private bool IsEnumerable(Type type)
            => type.GetInterface(nameof(IEnumerable)) != null;
    }
}
