using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.Rules;

namespace NConfig.Impl
{
    public class FilterPolicy : IFilterPolicy
    {
        public FilterPolicy()
        {
            this.Rules = new List<IFilterRule>();
        }
        public IList<IFilterRule> Rules { get; private set; }

        public IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, IDictionary<string, string> runtimeContext)
        {
            ILoggerFacade logger = Configure.Instance.Logger;
            logger.LogFormat("start filtering for context: {0} and items: {1}", runtimeContext.FormatString(), items.FormatString());

            IEnumerable<ParameterValue> filteredItems = items;
            foreach (IFilterRule rule in this.Rules)
            {
                logger.LogFormat("start using rule: {0}", rule.GetType().FullName);

                foreach (KeyValuePair<string, string> runtimeContextItem in runtimeContext)
                {
                    logger.LogFormat("start filtering by runtimeContextItem: {0}", runtimeContextItem.FormatString());

                    logger.LogFormat("items before: {0}", filteredItems.FormatString());

                    filteredItems = rule.Apply(filteredItems, runtimeContext, runtimeContextItem);

                    logger.LogFormat("items after: {0}", filteredItems.FormatString());
                }
            }

            return filteredItems;
        }
    }
}
