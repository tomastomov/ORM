using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface ITableNameProvider
    {
        string GetTableName(Type entity);

        void AddTableName(Type entity, string tableName);
    }
}
