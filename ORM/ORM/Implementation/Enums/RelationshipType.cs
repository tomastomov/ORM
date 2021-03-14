using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Enums
{
    public enum RelationshipType
    {
        None = 0,
        OneToOne = 1,
        OneToMany = 2,
        ManyToMany = 3
    }
}
