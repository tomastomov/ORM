using ORM.Contracts;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation
{
    internal class OrderByQueryTranslator : ExpressionVisitor, IQueryTranslator<LambdaExpression, string>
    {
        private readonly StringBuilder queryBuilder_;
        public OrderByQueryTranslator()
        {
            queryBuilder_ = new StringBuilder();
        }

        public string Translate(LambdaExpression query)
        {
            queryBuilder_.Append("ORDER BY ");
            Visit(query.Body);
            return queryBuilder_.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            queryBuilder_.Append(node.Member.Name);
            return node;
        }
    }
}