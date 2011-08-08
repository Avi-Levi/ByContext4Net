using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using System.IO;
using System.Diagnostics;

namespace NConfig.Filter.Rules
{
    public class FilterRuleTraceDecorator : IFilterRule
    {
        public FilterRuleTraceDecorator(IFilterRule inner)
        {
            this.Inner = inner; 
        }

        private IFilterRule Inner { get; set; }
        public IEnumerable<IHaveFilterReference> Apply(IEnumerable<IHaveFilterReference> items, 
            IDictionary<string, string> runtimeContext, KeyValuePair<string, string> currentRuntimeContextItem)
        {
            this.TraceState("Before",items, runtimeContext, currentRuntimeContextItem);

            IEnumerable<IHaveFilterReference> result = this.Inner.Apply(items, runtimeContext, currentRuntimeContextItem);

            this.TraceState("After", result, runtimeContext, currentRuntimeContextItem);

            return result;
        }

        private void TraceState(string stage, IEnumerable<IHaveFilterReference> items, 
            IDictionary<string, string> runtimeContext, KeyValuePair<string, string> currentRuntimeContextItem)
        {
            Debug.WriteLine(stage + " filtering using rule: " + this.Inner.GetType().FullName);
            Debug.WriteLine("Items: " + items.FormatString());
            Debug.WriteLine("Runtime context: " + runtimeContext.FormatString());
            Debug.WriteLine("Current runtime context item: " + currentRuntimeContextItem.FormatString());
        }
    }
}
