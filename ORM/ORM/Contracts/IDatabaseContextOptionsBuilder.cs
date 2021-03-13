using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IDatabaseContextOptionsBuilder
    {
        IDatabaseContextOptionsBuilder WithConnectionString(string connectionString);
    }
}
