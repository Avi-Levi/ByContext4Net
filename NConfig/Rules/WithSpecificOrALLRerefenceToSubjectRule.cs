using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public class WithSpecificOrALLRerefenceToSubjectRule : IFilterRule
    {
        public IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, IDictionary<string, string> runtimeContext, 
            KeyValuePair<string, string> currentRuntimeContextItem)
        {
            var query = from item in items
                        where
                        item.References.Any(currentRuntimeContextItem)
                        ||
                        item.References.Any(currentRuntimeContextItem.Key)
                        select item;
            return query;
        }
    }
}
