using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }

            return enumerable;
        }
    }
}
