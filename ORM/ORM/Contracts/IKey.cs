using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Contracts
{
    public interface IKey : IQueryTranslatable
    {
        Type Type { get; }

        PropertyInfo Property { get; }
    }
}
