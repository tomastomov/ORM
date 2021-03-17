using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class ConstraintTranslator : IConstraintTranslator
    {
        public string Translate(IConstraint constraint)
        {
            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine($"{SQLCommandConstants.ALTER_TABLE} {constraint.TableName}");

            queryBuilder.AppendLine($"{SQLCommandConstants.ADD_CONSTRAINT} {constraint.Name}");

            queryBuilder.AppendLine(constraint.Translate());

            return queryBuilder.ToString();
        }
    }
}
