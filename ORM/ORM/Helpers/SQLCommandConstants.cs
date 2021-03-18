using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Helpers
{
    internal static class SQLCommandConstants
    {
        public const string ALTER_TABLE = "ALTER TABLE";
        public const string ADD_CONSTRAINT = "ADD CONSTRAINT";
        public const string USE = "USE";
        public const string CREATE_TABLE = "CREATE TABLE";
        public const string PRIMARY_KEY_PREFIX = "PK_";
        public const string FOREIGN_KEY_PREFIX = "FK_";
        public const string PRIMARY_KEY = "PRIMARY KEY";
        public const string FOREIGN_KEY = "FOREIGN KEY";
    }
}
