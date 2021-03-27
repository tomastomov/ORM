using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class GenericExpressionVisitor : ExpressionVisitor, IExpressionVisitor
    {
        private string whereClause_;
        private string orderByClause_;
        string IExpressionVisitor.Visit(Expression expression)
        {
            Visit(expression);

            var queryBuilder = new StringBuilder();

            queryBuilder.AppendLine(whereClause_);
            queryBuilder.AppendLine(orderByClause_);

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
                    whereClause_ = whereTranslator.Translate(lambdaExpression);
                    Visit(node.Arguments[0]);
                break;
                case "FirstOrDefault":
                    break;
                case "OrderBy":
                    lambdaExpression = GetLambdaExpression(node);
                    var orderByTranslator = new OrderByQueryTranslator();
                    orderByClause_ = orderByTranslator.Translate(lambdaExpression);
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
