using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ORM.Extensions
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<T, K>(this Expression<Func<T, K>> propertyLambda)
        {
            var memberExpression = propertyLambda.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid property selector");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            return propertyInfo;
        }
    }
}
