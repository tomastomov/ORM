using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Implementation.Keys
{
    internal class ForeignKey : IKey
    {
        public ForeignKey(Type type, PropertyInfo property)
        {
            Type = type;
            Property = property;
        }

        public Type Type { get; private set; }

        public PropertyInfo Property { get; private set; }

        public string Translate()
        {
            return "FOREIGN KEY";
        }
    }
}
