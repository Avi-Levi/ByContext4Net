using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.Policy;

namespace NConfig.ValueSelector
{
    public class Filter
    {
        private readonly ILoggerFacade _logger = new OutputLogger();

        public IEnumerable<ISupportFilterPolicy> DoFilter(IEnumerable<ISupportFilterPolicy> items, IFilterPolicy policy,
            IDictionary<string, string> runtimeContext)
        {
            foreach (ISupportFilterPolicy item in items)
            {
                if (!this.CheckPolicy(policy, runtimeContext, item))
                {
                    this._logger.Log("item selected.");
                    yield return item;
                }
            }
        }

        private bool CheckPolicy(IFilterPolicy policy, IDictionary<string, string> runtimeContext, ISupportFilterPolicy item)
        {
            bool exclude = false;

            foreach (KeyValuePair<string, string> runtimeContextItem in runtimeContext)
            {
                this._logger.Log(string.Format("Processing context item with key:{0} and value: {1}.",
                    runtimeContextItem.Key, runtimeContextItem.Value));

                string notSatisfiedReasone = null;

                if (!policy.IsSatisfied(item.References, runtimeContextItem, out notSatisfiedReasone))
                {
                    exclude = true;
                    this._logger.Log(item.ToString() + " is excluded because: " + notSatisfiedReasone);
                }
            }
            return exclude;
        }
    }
}
