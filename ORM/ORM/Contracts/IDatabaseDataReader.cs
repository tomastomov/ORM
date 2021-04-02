using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IDatabaseDataReader
    {
        bool MoveNext();

        T Read<T>(string columnName);

        object Read(Type type, string columnName);
    }
}
