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
using ByContext.Filters.Conditions;

namespace ByContext.Filters.Evaluation
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

                return evaluationResult ? RelationToContextEnum.True : RelationToContextEnum.False;
            }

            return RelationToContextEnum.Neutral;
        } 
    }
}