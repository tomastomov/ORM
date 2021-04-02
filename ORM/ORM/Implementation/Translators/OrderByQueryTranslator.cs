using ORM.Contracts;
using ORM.Implementation.Enums;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Translators
{
    internal class OrderByQueryTranslator : ExpressionVisitor, IQueryTranslator<LambdaExpression, string>
    {
        private readonly StringBuilder queryBuilder_;
        private readonly SortingType sortingType_;
        public OrderByQueryTranslator(SortingType sortingType)
        {
            queryBuilder_ = new StringBuilder();
            sortingType_ = sortingType;
        }

        public string Translate(LambdaExpression query)
        {
            Visit(query.Body);
            queryBuilder_.Append($" {sortingType_}");
            return queryBuilder_.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            queryBuilder_.Append(node.Member.Name);
            return node;
        }
    }
}