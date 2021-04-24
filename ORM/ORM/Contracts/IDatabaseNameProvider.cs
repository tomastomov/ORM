using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    internal interface IDatabaseNameProvider
    {
        string DatabaseName { get; }
    }
}
