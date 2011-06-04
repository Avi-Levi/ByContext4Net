using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.Rules;

namespace NConfig.Policy
{
    public class FilterPolicy : IFilterPolicy
    {
        public FilterPolicy(IEnumerable<IFilterRule> rules)
        {
            this.Rules = rules;
        }

        private IEnumerable<IFilterRule> Rules { get; set; }

        public bool IsSatisfied(IList<ContextSubjectReference> references, 
            KeyValuePair<string, string> runtimeContextItem, out string notSatisfiedReasone)
        {
            Lazy<StringBuilder> sb = new Lazy<StringBuilder>();
            bool result = true;
            foreach (IFilterRule rule in this.Rules)
            {
                if (!rule.IsSatisfied(references, runtimeContextItem))
                {
                    sb.Value.AppendLine(rule.Description);
                    result = false;
                }
            }

            if (!result)
            {
                notSatisfiedReasone = sb.ToString();
            }
            else
            {
                notSatisfiedReasone = null;
            }

            return result;
        }
    }
}
