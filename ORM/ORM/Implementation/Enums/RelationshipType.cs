using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Enums
{
    public enum RelationshipType
    {
        One = 1,
        Many = 2,
        OneToOne = 3,
        OneToMany = 4,
        ManyToMany = 5
    }
}
