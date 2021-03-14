using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Enums
{
    [Flags]
    public enum RelationshipType
    {
        One = 1,
        OneToOne = 2,
        Many = 4,
        OneToMany = 5,
        ManyToMany = 8
    }
}
