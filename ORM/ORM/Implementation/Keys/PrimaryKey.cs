using ORM.Contracts;
using System;
using System.Reflection;

namespace ORM.Implementation.Keys
{
    internal class PrimaryKey : IKey
    {
        public PrimaryKey(Type type, PropertyInfo property)
        {
            Type = type;
            Property = property;
        }

        public Type Type { get; private set; }

        public PropertyInfo Property { get; private set; }

        public string Translate()
        {
            return "PRIMARY KEY";
        }
    }
}
