using ORM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ORM.Implementation
{
    public class SqlTypeConverter : ISqlTypeConverter
    {
        private static IList<SqlDataType> types_;
        private static long isPopulated_ = 0;
        public SqlTypeConverter()
        {
            if (Interlocked.CompareExchange(ref isPopulated_, 0, 0) == 0)
            {
                types_ = Assembly.GetAssembly(typeof(SqlDataType)).GetTypes()
                    .Where(t => typeof(SqlDataType).IsAssignableFrom(t) && !t.IsAbstract)
                    .Select(t => (SqlDataType)Activator.CreateInstance(t))
                    .ToList();

                Interlocked.Exchange(ref isPopulated_, 1);
            }
        }

        public SQLType Convert<TType, SQLType>(TType type) where SQLType : SqlDataType
        {
            return (SQLType)types_.FirstOrDefault(f => f.Type.ToString() == type.ToString());
        }
    }
}
