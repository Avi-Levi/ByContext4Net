using System.Collections.Generic;
using System.Linq;
using ByContext.FilterConditions;
using ByContext.FilterConditions.TextMatch;
using ByContext.Query;
using ByContext.Query.QueryEngine;
using ByContext.ValueProviders;
using NUnit.Framework;
using ByContext;

namespace ByContext.Tests
{
    [TestFixture]
    public class QueryEngineTests
    {
        [Test]
        public void SelectDefaultWhenNoItemWithHighestScoreFound()
        {
            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("env",null)}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{}), 
            }, false).Query(new Dictionary<string, string>());

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2, result.Single().Get());
        }
        [Test]
        public void with_two_items_while_one_is_with_one_true_condition_and_the_other_with_one_true_and_the_other_negated_true()
        {
            var context = new Dictionary<string, string>
                {
                    {"subject1", "a"},
                    {"subject2", "c"}
                };
            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]{new TextMatchCondition("subject1","a")}), 
                QueriableItem.Create(new DefaultValueProvider(2),new IFilterCondition[]{new TextMatchCondition("subject1","a"), new TextMatchCondition("subject2","b",true)}), 
            }, false)
            .Query(context);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2, result.Single().Get());
        }
        [Test]
        public void when_value_is_negated_and_another_value_with_the_same_subject_is_also_negated()
        {
            var context = new Dictionary<string, string>
                {
                    {"FamilyId", "40"},
                    {"CustomerId", "100"}
                };
            var result = new QueryEngineBuilder().Get(new[] { 
                QueriableItem.Create(new DefaultValueProvider(1),new IFilterCondition[]
                {
                    new TextMatchCondition("FamilyId","40",true), 
                    new TextMatchCondition("FamilyId","81",true), 
                    new TextMatchCondition("FamilyId","80",true),
                    new TextMatchCondition("CustomerId","7",true), 
                    new TextMatchCondition("CustomerId","12",true), 
                    new TextMatchCondition("CustomerId","19",true)})
            }, false)
            .Query(context);

            Assert.AreEqual(0, result.Length);
        }

    }

    public class DefaultValueProvider : IValueProvider
    {
        private readonly object _value;

        public DefaultValueProvider(object value)
        {
            _value = value;
        }

        public object Get()
        {
            return this._value;
        }
    }
}