using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    public class ModelData
    {
        public ModelData()
        {
            Relationships = new List<IEntityRelationship<Type, Type>>();
        }

        public ICollection<IEntityRelationship<Type, Type>> Relationships { get; private set; }
    }
}
