using ORM.Contracts;
using ORM.Helpers;
using ORM.Implementation.Keys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation.Constraints
{
    internal class ForeignKeyConstraint : IConstraint
    {
        private readonly IKey key_;
        public ForeignKeyConstraint(IKey foreignKey, string tableName, string referencedTableName)
        {
            key_ = foreignKey;
            TableName = tableName;
            ReferencedTableName = referencedTableName;
        }

        public string Name => $"{SQLCommandConstants.FOREIGN_KEY_PREFIX}{TableName}";

        public string TableName { get; }

        public string ReferencedTableName { get; private set; }

        public string Translate()
        {
            return $"{SQLCommandConstants.FOREIGN_KEY} ({key_.Property.Name}) REFERENCES {ReferencedTableName}(Id)";
        }
    }
}
