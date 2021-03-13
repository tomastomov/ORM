using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class TSqlCommand : ICommand
    {
        public string ConnectionString { get; set; }

        public string CommandText { get; set; }
    }
}
