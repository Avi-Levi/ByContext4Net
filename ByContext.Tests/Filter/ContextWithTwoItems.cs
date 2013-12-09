using System.Collections.Generic;
using System.Linq;
using ByContext.Exceptions;
using ByContext.FilterConditions;
using ByContext.FilterConditions.TextMatch;
using ByContext.Query;
using ByContext.Query.QueryEngine;
using NUnit.Framework;

namespace ByContext.Tests.Filter
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Any(x => x == 2));
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Any(x => x == 2));
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
            .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Any(x => x == 2));
            Assert.IsTrue(result.Any(x => x == 3));
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
.Query(context).Select(x => x.Get()).Cast<int>();


            Assert.IsTrue(result.Any(x => x == 2));
            Assert.IsTrue(result.Any(x => x == 3));
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
.Query(context).Select(x => x.Get()).Cast<int>();


            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(x => x == 2));
            Assert.IsTrue(result.Any(x => x == 3));
        }

        [Test]
        public void EmptyResultWhenSingleMatchingItemIsNegated()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",true)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("B","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","1",true)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("B","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count()== 1);
            Assert.IsTrue(result.Single() == 2);
        }

        [Test]
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherNot()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",true)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x == 3));
        }

        [Test]
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherWithoutAReference()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",true)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x == 3));
        }
        [Test]
        public void SelectSingleItemWhenOneItemHasOneSpecificReferenceAndTheOtherHasTwoSpecificReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                                  {"B", "2"},
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("B","2",false),new TextMatchCondition("A","2",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Single() == 2);
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("B","2",false),new TextMatchCondition("A","2",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x == 2));
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

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("B","2",false), new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","2",false), new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Any(x => x == 3));
        }
        [Test]
        public void SelectSingleWhenValueHasTwoReferencesToSameSubjectAndOnlyOneMatch()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","1",false),new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","3",false)}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","4",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Single() == 2);
        }

        [Test]
        public void when_two_negated_conditions_evaluates_to_true_select_them_over_the_default()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "2"},
                };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("B","5",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("B","3",true),new TextMatchCondition("B","4",true)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.AreEqual(1,result.Count());
            Assert.IsTrue(result.Any(x => x == 2));
        }
        [Test]
        public void SelectSingleWhenValueHasTwoReferencesToSameSubjectAndOnlyOneMatch2()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","3",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("B","3",false),new TextMatchCondition("B","4",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Single() == 2);
        }

        [Test]
        public void when_an_Item_has_no_references_prefer_it_over_other_items_with_nutral_references()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","3",true)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","5",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(x => x == 1));
        }
        [Test]
        [ExpectedException(typeof(ItemsWithConflictingHighestScoreException))]
        public void when_there_are_items_with_conflicting_highest_score_and_a_default_item_could_not_be_determined_throw()
        {
            var context = new Dictionary<string, string>
                {
                    {"B", "3"},
                };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","3",true)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","5",false)}), 
            }, false)
        .Query(context).Select(x => x.Get()).Cast<int>();
        }
    }
}