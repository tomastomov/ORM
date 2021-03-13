using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.SqlTypes
{
    public class SqlString : SqlDataType
    {
        public override Type Type => typeof(string);

        public override string SqlType => "varchar(255)";
    }
}
