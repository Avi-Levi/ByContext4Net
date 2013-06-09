// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Linq;
using ByContext.Filters.Evaluation;

namespace ByContext.Filters.Policy
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
