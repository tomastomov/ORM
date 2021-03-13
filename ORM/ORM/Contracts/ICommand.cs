using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface ICommand
    {
        string ConnectionString { get; }

        string CommandText { get; }
    }
}
