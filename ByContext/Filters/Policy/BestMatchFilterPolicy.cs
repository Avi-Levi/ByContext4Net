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
using ByContext.Exceptions;
using ByContext.Filters.Evaluation;

namespace ByContext.Filters.Policy
{
    public class BestMatchFilterPolicy : IFilterPolicy
    {
        public ItemEvaluation[] Filter(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            return this.FilterBestMatch(evaluatedItems).ToArray();
        }

        private IEnumerable<ItemEvaluation> FilterBestMatch(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            ItemEvaluation[] relevantItems = this.SelectOnlyRelevantItems(evaluatedItems).ToArray();
            if (relevantItems.Any())
            {
                yield return this.SelectBestMatchItem(relevantItems);
            }
        }

        private IEnumerable<ItemEvaluation> SelectOnlyRelevantItems(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            return new SelectAllRelevantFilterPolicy().Filter(evaluatedItems);
        }

        private ItemEvaluation SelectBestMatchItem(ItemEvaluation[] evaluatedItems)
        {
            var itemsWithCalculatedScore = CalculateScore(evaluatedItems);

            var highestScore = itemsWithCalculatedScore.Max(x => x.Score);

            var itemWithHighestScores = itemsWithCalculatedScore.Where(x => x.Score == highestScore).ToArray();

            ItemEvaluation? defaultItem;
            if (itemWithHighestScores.Count() > 1)
            {
                if (this.TrySelectDefault(itemsWithCalculatedScore, out defaultItem))
                {
                    return defaultItem.Value;
                }
                else
                {
                    throw new ItemsWithConflictingHighestScoreException(itemsWithConflictingScore: itemWithHighestScores, allItems: itemsWithCalculatedScore, highestScore: highestScore);
                }
            }

            return itemWithHighestScores.Single().Item;
        }

        private bool TrySelectDefault(ItemWithScore[] itemsWithCalculatedScore, out ItemEvaluation? defaultItem)
        {
            var itemsWithNoReferences = itemsWithCalculatedScore.Where(x => !x.Item.ConditionsEvaluation.Any());
            if (itemsWithNoReferences.Count() != 1)
            {
                defaultItem = null;
                return false;
            }
            defaultItem = itemsWithNoReferences.Single().Item;
            return true;
        }

        private ItemWithScore[] CalculateScore(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            var itemsWithCalculatedScore =
                evaluatedItems.Select(item =>
                    new ItemWithScore
                        (
                        item,
                        item.ConditionsEvaluation.Count(x => x.RelationToContext == RelationToContextEnum.True)
                        ))
                    ;
            return itemsWithCalculatedScore.ToArray();
        }
    }
}
