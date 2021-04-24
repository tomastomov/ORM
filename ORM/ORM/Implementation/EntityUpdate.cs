using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class EntityUpdate : IEntityUpdate
    {
        public EntityUpdate(string columnName, object updatedValue)
        {
            ColumnName = columnName;
            UpdatedValue = updatedValue;
        }
        public string ColumnName { get; private set; }
        public object UpdatedValue { get; private set; }
    }
}
