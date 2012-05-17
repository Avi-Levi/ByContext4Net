using System.Collections.Generic;

namespace NConfig.Filters.Evaluation
{
    public struct ConditionEvaluationContext
    {
        public readonly KeyValuePair<string, string> CurrentRuntimeContextItem;
        public readonly IDictionary<string, string> RuntimeContext;
        public readonly IEnumerable<IHaveFilterConditions> AllItems;

        public ConditionEvaluationContext(KeyValuePair<string, string> currentRuntimeContextItem, IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> allItems)
        {
            CurrentRuntimeContextItem = currentRuntimeContextItem;
            RuntimeContext = runtimeContext;
            AllItems = allItems;
        }
    }
}