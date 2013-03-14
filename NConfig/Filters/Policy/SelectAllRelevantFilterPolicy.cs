using System.Collections.Generic;
using System.Linq;
using NConfig.Filters.Evaluation;

namespace NConfig.Filters.Policy
{
    public class SelectAllRelevantFilterPolicy : IFilterPolicy
    {
        public ItemEvaluation[] Filter(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            return evaluatedItems.Where(IsRelevant).ToArray();
        }

        private bool IsRelevant(ItemEvaluation itemEvaluation)
        {
            var groupedBySubject = itemEvaluation.ConditionsEvaluation.GroupBy(x => x.Context.CurrentRuntimeContextItem.Key);

            foreach (var group in groupedBySubject)
            {
                if(!IsRelevantToSubject(group))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsRelevantToSubject(IGrouping<string, ConditionEvaluation> group)
        {
            var groupedByIsNegated = @group.GroupBy(x => x.Condition.Negate).ToArray();
            var negatedConditions = groupedByIsNegated.SingleOrDefault(x => x.Key);
            if (negatedConditions != null && negatedConditions.Any(x => x.RelationToContext == RelationToContextEnum.False))
            {
                return false;
            }

            var notNegatedConditions = groupedByIsNegated.SingleOrDefault(x => !x.Key);
            if (notNegatedConditions != null && notNegatedConditions.Any(x => x.RelationToContext == RelationToContextEnum.False))
            {
                return notNegatedConditions.Any(x => x.RelationToContext == RelationToContextEnum.True);
            }

            return true;
        }
    }
}
