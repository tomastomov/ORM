using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class SqlCommandOptionsBuilder : ICommandOptionsBuilder
    {
        private TSqlCommand command_;
        public SqlCommandOptionsBuilder(TSqlCommand command = null)
        {
            command_ = command ?? new TSqlCommand();
        }
        public ICommandOptionsBuilder WithCommandText(string commandText)
        {
            command_.CommandText = commandText;
            return this;
        }

        public ICommandOptionsBuilder WithConnectionString(string connectionString)
        {
            command_.ConnectionString = connectionString;
            return this;
        }

        internal ICommand Build()
            => command_;
    }
}
