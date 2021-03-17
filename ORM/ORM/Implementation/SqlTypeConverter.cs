using ORM.Contracts;
using ORM.Implementation.SqlTypes;
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
        static SqlTypeConverter()
        {
            types_ = Assembly.GetAssembly(typeof(SqlDataType)).GetTypes()
                    .Where(t => typeof(SqlDataType).IsAssignableFrom(t) && !t.IsAbstract)
                    .Select(t => (SqlDataType)Activator.CreateInstance(t))
                    .ToList();
        }

        public SQLType Convert<TType, SQLType>(TType type) where SQLType : SqlDataType
        {
            return (SQLType)types_.FirstOrDefault(f => f.Type.ToString() == type.ToString());
        }
    }
}
