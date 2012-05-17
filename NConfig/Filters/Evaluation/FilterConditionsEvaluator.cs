using System.Collections.Generic;
using System.Linq;
using NConfig.Filters.Conditions;

namespace NConfig.Filters.Evaluation
{
    public class FilterConditionsEvaluator : IFilterConditionsEvaluator
    {
        public IEnumerable<ItemEvaluation> Evaluate(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items)
        {
            foreach (IHaveFilterConditions item in items)
            {
                var conditionsEvaluationResult = EvaluateItemConditions(item, runtimeContext, items).ToArray();
                yield return new ItemEvaluation(item, conditionsEvaluationResult);
            }
        }

        private IEnumerable<ConditionEvaluation> EvaluateItemConditions(IHaveFilterConditions currentItem, IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items)
        {
            foreach (KeyValuePair<string, string> runtimeContextItem in runtimeContext)
            {
                foreach (var condition in currentItem.FilterConditions)
                {
                    var evalContext = new ConditionEvaluationContext(runtimeContextItem, runtimeContext, items);
                    var relationToContext = EvaluateCondition(evalContext, condition);
                    yield return new ConditionEvaluation(relationToContext, evalContext, condition);
                }
            }
        }

        private RelationToContextEnum EvaluateCondition(ConditionEvaluationContext context, IFilterCondition condition)
        {
            if (condition.Subject == context.CurrentRuntimeContextItem.Key)
            {
                bool evaluationResult = condition.Evaluate(context);
                if (condition.Negate)
                {
                    evaluationResult = !evaluationResult;
                }

                if (evaluationResult)
                {
                    return RelationToContextEnum.True;
                }

                return RelationToContextEnum.False;
            }

            return RelationToContextEnum.Neutral;
        } 
    }
}