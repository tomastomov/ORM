using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class TableNameProvider : ITableNameProvider
    {
        private readonly IDictionary<Type, string> typeToTableName_;
        public TableNameProvider()
        {
            typeToTableName_ = new Dictionary<Type, string>();
        }

        public void AddTableName(Type entity, string tableName)
        {
            if (!typeToTableName_.ContainsKey(entity))
            {
                typeToTableName_.Add(entity, tableName);
            }
            else typeToTableName_[entity] = tableName;
        }

        public string GetTableName(Type entity)
        {
            if (typeToTableName_.TryGetValue(entity, out var name))
            {
                return name;
            }

            return null;
        }
    }
}
