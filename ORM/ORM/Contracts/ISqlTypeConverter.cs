using ORM.Implementation.SqlTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface ISqlTypeConverter
    {
        SQLType Convert<TType, SQLType>(TType type) where SQLType : SqlDataType;
    }
}
