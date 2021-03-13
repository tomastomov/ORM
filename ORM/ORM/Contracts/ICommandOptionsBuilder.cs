using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface ICommandOptionsBuilder
    {
        ICommandOptionsBuilder WithConnectionString(string connectionString);

        ICommandOptionsBuilder WithCommandText(string commandText);
    }
}
