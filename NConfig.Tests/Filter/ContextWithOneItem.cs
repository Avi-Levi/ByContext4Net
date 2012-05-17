using System.Collections.Generic;
using System.Linq;
using NConfig.Filters.Conditions.TextMatch;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NUnit.Framework;

namespace NConfig.Tests.Filter
{
    [TestFixture]
    public class ContextWithOneItem
    {
        [Test]
        public void SelectSingleItemWithOneSpecificReference()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
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
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherNot()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
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
   }
}