using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Reflection;

namespace ORM.Implementation.Keys
{
    internal class PrimaryKey : IKey
    {
        public PrimaryKey(Type dataType, PropertyInfo property)
        {
            DataType = dataType;
            Property = property;
        }

        public Type DataType { get; private set; }

        public PropertyInfo Property { get; private set; }
    }
}
