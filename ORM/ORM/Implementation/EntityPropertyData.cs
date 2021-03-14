using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Implementation
{
    public class EntityPropertyData
    {
        public PropertyInfo Property { get; set; }
        public bool IsIgnored { get; set; }
    }
}
