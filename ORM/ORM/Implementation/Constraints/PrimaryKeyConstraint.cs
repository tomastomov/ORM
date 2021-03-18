using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Constraints
{
    internal class PrimaryKeyConstraint : IConstraint
    {
        private readonly IKey key_;
        public PrimaryKeyConstraint(IKey primaryKey, string tableName)
        {
            key_ = primaryKey;
            TableName = tableName;
        }
        public string Name => $"{SQLCommandConstants.PRIMARY_KEY_PREFIX}{TableName}";

        public string TableName { get; }

        public string Translate()
        {
            return $"PRIMARY KEY ({key_.Property.Name})";
        }
    }
}
