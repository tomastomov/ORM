using ORM.Contracts;
using ORM.Implementation.Enums;
using ORM.Implementation.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class GenericExpressionVisitor : ExpressionVisitor, IExpressionVisitor
    {
        private ICollection<string> orderByClauses_;

        public GenericExpressionVisitor()
        {
            orderByClauses_ = new List<string>();
        }
        public string WhereClause { get; private set; }
        public string SelectClause { get; private set; }

        public string FirstOrDefaultClause { get; private set; }

        string IExpressionVisitor.Visit(Expression expression)
        {
            Visit(expression);

            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine(WhereClause);
            queryBuilder.Append("ORDER BY ");
            orderByClauses_ = orderByClauses_.Reverse().ToList();
            return orderByClauses_.Aggregate(queryBuilder, (builder, next) =>
            {
                builder.Append($"{next},");
                return builder;
            }, builder => builder.ToString().TrimEnd(','));
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.NodeType == ExpressionType.Call)
            {
                return this.VisitMethodCall((MethodCallExpression)node);
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var name = node.Method.Name;
            LambdaExpression lambdaExpression;

            switch (name)
            {
                case "Where":
                    lambdaExpression = GetLambdaExpression(node);
                    var whereTranslator = new WhereQueryTranslator();
                    WhereClause = whereTranslator.Translate(lambdaExpression);
                    Visit(node.Arguments[0]);
                break;
                case "FirstOrDefault":
                    FirstOrDefaultClause = "TOP 1 *";
                    Visit(node.Arguments[0]);
                    break;
                case "ThenBy":
                case "OrderBy":
                case "OrderByDescending":
                case "ThenByDescending":
                    var sortingType = name.Contains("Descending") ? SortingType.DESC : SortingType.ASC;
                    lambdaExpression = GetLambdaExpression(node);
                    var orderByTranslator = new OrderByQueryTranslator(sortingType);
                    orderByClauses_.Add(orderByTranslator.Translate(lambdaExpression));
                    Visit(node.Arguments[0]);
                    break;
                case "Select":
                    lambdaExpression = GetLambdaExpression(node);
                    var selectTranslator = new SelectQueryTranslator();
                    SelectClause = selectTranslator.Translate(lambdaExpression);
                    Visit(node.Arguments[0]);
                    break;
                default:
                    break;
            }

            return node;
        }

        private LambdaExpression GetLambdaExpression(MethodCallExpression methodCall)
        {
            var unaryExpression = methodCall.Arguments[1] as UnaryExpression;
            return unaryExpression.Operand as LambdaExpression;
        }
    }
}
