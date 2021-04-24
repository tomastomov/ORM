﻿using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class UpdatedEntity : IUpdatedEntity
    {
        public UpdatedEntity(string tableName, IEnumerable<IEntityUpdate> updates, Expression<Func<object, bool>> dbIdentifier)
        {
            TableName = tableName;
            EntityUpdates = updates;
            DbIdentifier = dbIdentifier;
        }
        public string TableName { get; private set; }

        public IEnumerable<IEntityUpdate> EntityUpdates { get; private set; }

        public Expression<Func<object, bool>> DbIdentifier { get; private set; }
    }
}
