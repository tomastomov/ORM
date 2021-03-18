using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Implementation.Keys
{
    internal class ForeignKey : IKey
    {
        public ForeignKey(Type dataType, PropertyInfo property, IEntityRelationship relationship)
        {
            DataType = dataType;
            Property = property;
            Relationship = relationship;
        }

        public Type DataType { get; }

        public PropertyInfo Property { get; }

        public IEntityRelationship Relationship { get; }
    }
}
