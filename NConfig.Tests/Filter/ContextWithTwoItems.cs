using System.Collections.Generic;
using System.Linq;
using NConfig.Exceptions;
using NConfig.Filters.Conditions.TextMatch;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NUnit.Framework;

namespace NConfig.Tests.Filter
{
    [TestFixture]
    public class ContextWithTwoItems
    {
        [Test]
        public void SelectSingleItemWithOneSpecificReference()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2")),
                                new Item(3,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Any(x => x.Id == 2));
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void SelectSingleItemWithNoReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2),
                                new Item(3,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Any(x => x.Id == 2));
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void SelectTwoItemsWithReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2")),
                                new Item(3,new TextMatchCondition("A","2")),
                                new Item(4,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Any(x => x.Id == 2));
            Assert.IsTrue(result.Any(x => x.Id == 3));
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SelectTwoItemsWithOutReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2),
                                new Item(3),
                                new Item(4,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Any(x => x.Id == 2));
            Assert.IsTrue(result.Any(x => x.Id == 3));
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void SelectTwoItemsOneWithReferencesAndOneWithoutAnyReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2")),
                                new Item(3),
                                new Item(4,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Any(x => x.Id == 2));
            Assert.IsTrue(result.Any(x => x.Id == 3));
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void EmptyResultWhenSingleMatchingItemIsNegated()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2",true)),
                                new Item(3,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsFalse(result.Any());
        }

        [Test]
        public void EmptyResultWhenSingleNoneMatchingItemIsNegated()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("B","1")),
                                new Item(2,new TextMatchCondition("A","1",true)),
                                new Item(3,new TextMatchCondition("B","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count()== 1);
            Assert.IsTrue(result.Single().Id == 2);
        }

        [Test]
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherNot()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2",true)),
                                new Item(3,new TextMatchCondition("A","2")),
                                new Item(4,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 3));
        }

        [Test]
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherWithoutAReference()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2",true)),
                                new Item(3),
                                new Item(4,new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 3));
        }
        [Test]
        public void SelectSingleItemWhenOneItemHasOneSpecificReferenceAndTheOtherHasTwoSpecificReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","2")),
                                new Item(2,new TextMatchCondition("B","2"),new TextMatchCondition("A","2")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 2));
        }
        [Test]
        public void SelectSingleItemWhenOneItemHasOneSpecificReferenceAndTheOtherHasTwoSpecificReferencesAndNoReferenecsToOtherSubjects()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                                  {"C", "2"},
                                  {"D", "2"},
                                  {"E", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","2")),
                                new Item(2,new TextMatchCondition("B","2"),new TextMatchCondition("A","2")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 2));
        }
        [Test]
        public void SelectTwoItemsThatHasAMatchingReferenceToASubjectAndOneOfThemHasAlsoAReferenceToTheSameSubjectButWithDifferentValue()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                                  {"C", "2"},
                                  {"D", "2"},
                                  {"E", "2"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","2")),
                                new Item(2,new TextMatchCondition("B","2"),new TextMatchCondition("A","1")),
                                new Item(3,new TextMatchCondition("A","2"),new TextMatchCondition("A","1")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new SelectAllRelevantFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(x => x.Id == 1));
            Assert.IsTrue(result.Any(x => x.Id == 3));
        }
        [Test]
        public void SelectSingleWhenValueHasTwoReferencesToSameSubjectAndOnlyOneMatch()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                              };

            var items = new[]
                            {
                                new Item(1),
                                new Item(2,new TextMatchCondition("A","1"),new TextMatchCondition("A","2")),
                                new Item(3,new TextMatchCondition("A","3")),
                                new Item(4,new TextMatchCondition("A","4")),
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 2));
        }

        [Test]
        public void when_two_negated_conditions_evaluates_to_true_select_them_over_the_default()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "2"},
                };

            var items = new[]
                {
                    new Item(1, new TextMatchCondition("B", "5")),

                    new Item(2, new TextMatchCondition("B", "3",true), new TextMatchCondition("B", "4",true)),
                };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 2));
        }
        [Test]
        public void SelectSingleWhenValueHasTwoReferencesToSameSubjectAndOnlyOneMatch2()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var items = new[]
                {
                    new Item(1),

                    new Item(2, new TextMatchCondition("B", "3",true), new TextMatchCondition("B", "4",true)),
                };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 1));
        }

        [Test]
        public void when_an_Item_has_no_references_prefer_it_over_other_items_with_nutral_references()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var items = new[]
                {
                    new Item(1),

                    new Item(2, new TextMatchCondition("A", "3",true)),
                    new Item(2, new TextMatchCondition("A", "5")),
                };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x.Id == 1));
        }
        [Test]
        [ExpectedException(typeof(ItemsWithConflictingHighestScoreException))]
        public void when_there_are_items_with_conflicting_highest_score_and_a_default_item_could_not_be_determined_throw()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var items = new[]
                {
                    new Item(2, new TextMatchCondition("A", "3",true)),
                    new Item(2, new TextMatchCondition("A", "5")),
                };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            new BestMatchFilterPolicy().Filter(evaluationResult);
        }
    }
}