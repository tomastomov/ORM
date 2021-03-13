using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Contracts
{
    public interface IDatabase
    {
        ICommand CreateCommand(Action<ICommandOptionsBuilder> builder);

        ICommandExecutionResult ExecuteCommand(ICommand command);
    }
}
