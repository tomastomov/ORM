using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Contracts
{
    public interface IKey
    {
        Type DataType { get; }

        PropertyInfo Property { get; }
    }
}
