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
            Relationships = new List<IEntityRelationship>();
            Keys = new List<IKey>();
            EntityPropertiesData = new List<EntityPropertyData>();
        }

        public ICollection<IEntityRelationship> Relationships { get; private set; }

        public ICollection<IKey> Keys { get; private set; }

        public ICollection<EntityPropertyData> EntityPropertiesData { get; private set; }
    }
}
