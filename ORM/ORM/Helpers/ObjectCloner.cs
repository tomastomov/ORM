using ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Helpers
{
    internal static class ObjectCloner
    {
        internal static object Clone(object instance)
        {
            var newInstance = Activator.CreateInstance(instance.GetType());
            instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Each(property =>
                {
                    newInstance.GetType().GetProperty(property.Name).SetValue(newInstance, property.GetValue(instance));
                });

            return newInstance;
        }
    }
}
