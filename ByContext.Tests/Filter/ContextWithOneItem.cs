using System.Collections.Generic;
using System.Linq;
using ByContext.FilterConditions;
using ByContext.FilterConditions.TextMatch;
using ByContext.Query;
using ByContext.Query.QueryEngine;
using NUnit.Framework;

namespace ByContext.Tests.Filter
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

            
            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
        .Query(context).Select(x=>x.Get()).Cast<int>();

            Assert.IsTrue(result.Any(x => x == 2));
            Assert.IsTrue(result.Count() == 1);
        }
        [Test]
        public void SelectSingleItemWithNoReferences()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
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
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2")),
                                new Item(3,new TextMatchCondition("A","2")),
                                new Item(4,new TextMatchCondition("A","1")),
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
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2),
                                new Item(3),
                                new Item(4,new TextMatchCondition("A","1")),
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
                              };

            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("A","2",false)}), 
                QueriableItem.Create(new DefaultValueProvider(3),new IFilterCondition[]{}), 
                QueriableItem.Create(new DefaultValueProvider(4),new IFilterCondition[]{new TextMatchCondition("A","1",false)}), 
            }, true)
                .Query(context).Select(x => x.Get()).Cast<int>();

            Assert.IsTrue(result.Any(x => x == 2));
            Assert.IsTrue(result.Any(x => x == 3));
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void EmptyResultWhenSingleMatchingItemIsNegated()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
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
        public void SelectSingleItemWhenOneMatchIsNegatedAndTheOtherNot()
        {
            var context = new Dictionary<string, string>
                              {
                                  {"A", "2"},
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
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1")),
                                new Item(2,new TextMatchCondition("A","2",true)),
                                new Item(3),
                                new Item(4,new TextMatchCondition("A","1")),
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
   }
}