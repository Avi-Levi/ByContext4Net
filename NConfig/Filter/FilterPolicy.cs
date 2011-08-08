using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using NConfig.Filter.Rules;

namespace NConfig.Filter
{
    public class FilterPolicy : IFilterPolicy
    {
        public FilterPolicy(IFilterRule[] rules)
        {
            this.Rules = rules;
        }

        private IFilterRule[] Rules { get; set; } 

        public IEnumerable<IHaveFilterReference> Filter(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterReference> items)
        {
            IEnumerable<IHaveFilterReference> filteredItems = items;

            foreach (IFilterRule rule in this.Rules)
            {
                foreach (KeyValuePair<string, string> runtimeContextItem in runtimeContext)
                {
                    filteredItems = rule.Apply(filteredItems, runtimeContext, runtimeContextItem);
                }
            }

            return filteredItems;
        }
    }
}
