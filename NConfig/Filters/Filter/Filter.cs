using System.Collections.Generic;
using System.Linq;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;

namespace NConfig.Filters.Filter
{
    public class Filter : IFilter
    {
        public Filter(IFilterPolicy policy, IFilterConditionsEvaluator filterConditionsEvaluator)
        {
            Policy = policy;
            FilterConditionsEvaluator = filterConditionsEvaluator;
        }

        private IFilterPolicy Policy { get; set; }
        private IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        
        public IHaveFilterConditions[] FilterItems(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items)
        {
            IEnumerable<ItemEvaluation> itemsEvaluationResult = this.FilterConditionsEvaluator.Evaluate(runtimeContext, items);

            return this.Policy.Filter(itemsEvaluationResult).Select(x => x.Item).ToArray();
        }
    }
}