using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface ICommandExecutionResult
    {
        bool Success { get; }

        Exception Error { get; }
    }
}
