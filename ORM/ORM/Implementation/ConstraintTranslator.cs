using ORM.Contracts;
using ORM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Implementation
{
    internal class ConstraintTranslator : IConstraintTranslator
    {
        private readonly DatabaseContextOptions options_;

        public ConstraintTranslator(DatabaseContextOptions options)
        {
            options_ = options;
        }

        public string Translate(IConstraint constraint)
        {
            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine($"{SQLCommandConstants.USE} {options_.DatabaseName}");

            queryBuilder.AppendLine($"{SQLCommandConstants.ALTER_TABLE} {constraint.TableName}");

            queryBuilder.AppendLine($"{SQLCommandConstants.ADD_CONSTRAINT} {constraint.Name}");

            queryBuilder.AppendLine(constraint.Translate());

            return queryBuilder.ToString();
        }
    }
}
