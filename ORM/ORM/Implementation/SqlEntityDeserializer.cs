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
                entities.Add(entityInstance);
            }

            return !isEnumerable ? entities[0] : entities;
        }

        private bool IsEnumerable(Type type)
            => type.GetInterface(nameof(IEnumerable)) != null;
    }
}
