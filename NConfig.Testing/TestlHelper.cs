using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NConfig.Testing
{
    public static class TestlHelper
    {
        public static IEnumerable<TAttribute> ExtractAttributes<TAttribute>(MethodBase method) where TAttribute : Attribute
        {
            return method.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>();
        }
        public static IDictionary<string, string> ExtractRuntimeContextFromMethod(MethodBase method)
        {
            return TestlHelper.ExtractAttributes<RuntimeContextItemAttribute>(method).ToDictionary(x => x.Name, x => x.Value);
        }
    }
}
