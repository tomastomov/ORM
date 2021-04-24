using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IEntityUpdate
    {
        string ColumnName { get; }

        object UpdatedValue { get; }
    }
}
