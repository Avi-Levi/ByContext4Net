using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NConfig.Filters;
using NConfig.Filters.Rules;
using NConfig.Model;
using NConfig.Testing;
using NConfig.Tests.Filter;

namespace NConfig.Tests.Helpers
{
    internal static class Helper
    {
        internal static IFilterPolicy ExtractFilterPolicyFromMethod(MethodBase method)
        {
            var rules = TestlHelper.ExtractAttributes<FilterPolicyAttribute>(method).Single()
                .Rules.Select(ruleType => (IFilterRule)Activator.CreateInstance(ruleType)).ToArray();

            return new FilterPolicy(rules);
        }

        internal static KeyValuePair<string, string> ExtractRuntimeContextItemToFilterByFromMethod(MethodBase method)
        {
            return TestlHelper.ExtractAttributes<RuntimeContextItemToFilterByAttribute>(method).Single().Item;
        }

        internal static IEnumerable<ParameterValue> ExtractValuesFromMethod(MethodBase method)
        {
            var containerClassType = TestlHelper.ExtractAttributes<ParameterValuesTypeAttribute>(method).Single().ContainerClassType;
            
            return containerClassType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetValue(null)).OfType<ParameterValue>().ToArray();
        }


    }
}
