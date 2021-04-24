using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Translators
{
    internal class WhereQueryTranslator : ExpressionVisitor, IQueryTranslator<LambdaExpression, string>
    {
        private readonly StringBuilder queryBuilder_;
        public WhereQueryTranslator()
        {
            queryBuilder_ = new StringBuilder();
        }
        public string Translate(LambdaExpression query)
        {
            queryBuilder_.Clear();
            queryBuilder_.Append("WHERE ");
            var body = query.Body as BinaryExpression;

            VisitBinary(body);

            return queryBuilder_.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            queryBuilder_.Append("(");
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    queryBuilder_.Append(" AND ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    queryBuilder_.Append(" OR ");
                    break;
                case ExpressionType.GreaterThan:
                    queryBuilder_.Append(" > ");
                    break;
                case ExpressionType.Equal:
                    queryBuilder_.Append(" = ");
                    break;
                default:
                    break;
            }

            Visit(node.Right);

            queryBuilder_.Append(")");

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value.GetType() == typeof(string))
            {
                queryBuilder_.Append($"'{node.Value}'");

            }
            else
            {
                queryBuilder_.Append($"{node.Value}");
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            queryBuilder_.Append(node.Member.Name);

            return node;
        }
    }
}
