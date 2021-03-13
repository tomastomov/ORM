using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    public class SqlInt : SqlDataType
    {
        public override Type Type => typeof(int);
        public override string SqlType => "int";
    }
}
