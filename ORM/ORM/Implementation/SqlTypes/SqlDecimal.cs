using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.SqlTypes
{
    internal class SqlDecimal : SqlDataType
    {
        public override Type Type => typeof(decimal);

        public override string SqlType => "decimal";
    }
}
