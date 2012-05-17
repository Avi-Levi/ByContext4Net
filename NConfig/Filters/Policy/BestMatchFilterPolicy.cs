using System.Collections.Generic;
using System.Linq;
using NConfig.Exceptions;
using NConfig.Filters.Evaluation;

namespace NConfig.Filters.Policy
{
    public class BestMatchFilterPolicy : IFilterPolicy
    {
        public IEnumerable<ItemEvaluation> Filter(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            IEnumerable<ItemEvaluation> relevantItems = this.SelectOnlyRelevantItems(evaluatedItems);

            return this.SelectBestMatchItem(relevantItems);
        }

        private IEnumerable<ItemEvaluation> SelectOnlyRelevantItems(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            return new SelectAllRelevantFilterPolicy().Filter(evaluatedItems);
        }

        private IEnumerable<ItemEvaluation> SelectBestMatchItem(IEnumerable<ItemEvaluation> evaluatedItems)
        {
            var itemsWithCalculatedScore = CalculateScore(evaluatedItems);

            VerifyNoItemsWithSameScore(itemsWithCalculatedScore);

            if (itemsWithCalculatedScore.Any())
            {
                var highestScore = itemsWithCalculatedScore.Max(x => x.Score);

                return new[] { itemsWithCalculatedScore.Single(x => x.Score == highestScore).Item };
            }
            else
            {
                return new ItemEvaluation[]{};
            }
        }

        private void VerifyNoItemsWithSameScore(ItemWithScore[] itemsWithCalculatedScore)
        {
            var itemsGroupedByScore = itemsWithCalculatedScore.GroupBy(x => x.Score);
            var itemsWithSameScore = itemsGroupedByScore.Where(x => x.Count() != 1);
            if (itemsWithSameScore.Any())
            {
                throw new ItemsWithConflictingMatchingScoreException(itemsWithSameScore.Select(x => x.Key));
            }
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
