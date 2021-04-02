using ORM.Contracts;
using ORM.Implementation.Translators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class GenericExpressionVisitor : ExpressionVisitor, IExpressionVisitor
    {
        public string WhereClause { get; private set; }
        public string OrderByClause { get; private set; }
        public string SelectClause { get; private set; }

        string IExpressionVisitor.Visit(Expression expression)
        {
            Visit(expression);

            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine(WhereClause);
            queryBuilder.AppendLine(OrderByClause);

            return queryBuilder.ToString();
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
                    break;
                case "OrderBy":
                    lambdaExpression = GetLambdaExpression(node);
                    var orderByTranslator = new OrderByQueryTranslator();
                    OrderByClause = orderByTranslator.Translate(lambdaExpression);
                    Visit(node.Arguments[0]);
                    break;
                case "Select":
                    var br = node;
                    lambdaExpression = GetLambdaExpression(br);
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
