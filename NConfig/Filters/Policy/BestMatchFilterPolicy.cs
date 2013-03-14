using System.Collections.Generic;
using System.Linq;
using NConfig.Exceptions;
using NConfig.Filters.Evaluation;

namespace NConfig.Filters.Policy
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
