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
        Expression IExpressionVisitor.Visit(Expression expression)
        {
            return Visit(expression);
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return null;
            }

            var mc = (MethodCallExpression)node;

            if (node.NodeType == ExpressionType.Call)
            {
                return this.VisitMethodCall((MethodCallExpression)node);
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var name = node.Method.Name;

            switch (name)
            {
                case "Where":
                    var unaryExpression = node.Arguments[1] as UnaryExpression;
                    var lambdaExpression = unaryExpression.Operand as LambdaExpression;
                    var whereTranslator = new WhereQueryTranslator();
                    whereClause_ = whereTranslator.Translate(lambdaExpression);
                break;
                default:
                    break;
            }

            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {

            return node;
        }
    }
}
