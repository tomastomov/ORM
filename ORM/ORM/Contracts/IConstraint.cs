using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IConstraint : IQueryTranslatable
    {
        string Name { get; }

        string TableName { get; }
    }
}
