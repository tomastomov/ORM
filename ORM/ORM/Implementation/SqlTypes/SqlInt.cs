using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.SqlTypes
{
    public class SqlInt : SqlDataType
    {
        public override Type Type => typeof(int);
        public override string SqlType => "int";
    }
}
