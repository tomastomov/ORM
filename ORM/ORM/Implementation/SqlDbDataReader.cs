using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlDbDataReader : IDatabaseDataReader
    {
        private readonly DbDataReader sqlDataReader_;
        public SqlDbDataReader(DbDataReader dbDataReader)
        {
            sqlDataReader_ = dbDataReader;
        }

        public T Read<T>(string columnName)
        {
            return (T)sqlDataReader_[columnName];
        }

        public bool MoveNext()
            => sqlDataReader_.Read();

        public object Read(Type type, string columnName)
            => Convert.ChangeType(sqlDataReader_[columnName], type);
    }
}
