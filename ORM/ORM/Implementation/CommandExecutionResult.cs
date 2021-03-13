using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class CommandExecutionResult : ICommandExecutionResult
    {
        public CommandExecutionResult(bool success, Exception error = null)
        {
            Success = success;
            Error = error
        }

        public bool Success { get; private set; }

        public Exception Error { get; private set; }
    }
}
