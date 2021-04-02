using ORM.Contracts;
using ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Implementation.Translators
{
    internal class SelectQueryTranslator : ExpressionVisitor, IQueryTranslator<LambdaExpression, string>
    {
        private readonly StringBuilder queryBuilder_;
        public SelectQueryTranslator()
        {
            queryBuilder_ = new StringBuilder();
        }

        public string Translate(LambdaExpression query)
        {
            var memberInitExpression = query.Body as MemberInitExpression;

            VisitMemberInit(memberInitExpression);

            return queryBuilder_.ToString().TrimEnd(' ').TrimEnd(',');
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            node.Bindings.Each(binding => queryBuilder_.Append($"{binding.Member.Name}, "));
            return base.VisitMemberInit(node);
        }
    }
}
