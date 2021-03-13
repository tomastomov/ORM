using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.SqlTypes
{
    public abstract class SqlDataType
    {
        public abstract Type Type { get; }

        public abstract string SqlType { get; }
    }
}
