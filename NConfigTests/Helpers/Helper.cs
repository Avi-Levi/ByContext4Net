using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NConfig.Filter;
using NConfig.Filter.Rules;
using NConfig.Model;

namespace NConfig.Tests.Helpers
{
    internal static class Helper
    {
        internal static IDictionary<string, string> ExtractRuntimeContextFromMethod(MethodBase method)
        {
            return ExtractAttributes<RuntimeContextItemAttribute>(method).ToDictionary(x => x.Name, x => x.Value);
        }

        internal static IFilterPolicy ExtractFilterPolicyFromMethod(MethodBase method)
        {
            var rules = ExtractAttributes<FilterPolicyAttribute>(method).Single()
                .Rules.Select(ruleType => (IFilterRule)Activator.CreateInstance(ruleType)).ToArray();

            return new FilterPolicy(rules);
        }

        internal static KeyValuePair<string, string> ExtractRuntimeContextItemToFilterByFromMethod(MethodBase method)
        {
            return ExtractAttributes<RuntimeContextItemToFilterByAttribute>(method).Single().Item;
        }

        internal static IEnumerable<ParameterValue> ExtractValuesFromMethod(MethodBase method)
        {
            var containerClassType = ExtractAttributes<ParameterValuesTypeAttribute>(method).Single().ContainerClassType;
            
            return containerClassType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null)).OfType<ParameterValue>().ToArray();
        }

        private static IEnumerable<TAttribute> ExtractAttributes<TAttribute>(MethodBase method) where TAttribute : Attribute
        {
            return method.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>();
        }
    }
}
