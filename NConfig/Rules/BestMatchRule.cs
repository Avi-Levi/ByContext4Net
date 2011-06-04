using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public class BestMatchRule : IFilterRule
    {
        public IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, IDictionary<string, string> runtimeContext,
            KeyValuePair<string, string> currentRuntimeContextItem)
        {
            if(items.Any(currentRuntimeContextItem) && items.Any(currentRuntimeContextItem.Key))
            {
                return items.Filter(currentRuntimeContextItem);
            }
            else
            {
                return items;
            }
        }
    }
}
