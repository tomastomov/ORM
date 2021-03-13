using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public class DatabaseContextOptions
    {
        public DatabaseContextOptions(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string DatabaseName { get; private set; }
        public string ConnectionString { get; private set; }
    }
}
