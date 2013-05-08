using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Filters.Conditions.TextMatch;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NConfig.Tests.Filter;
using NUnit.Framework;

namespace NConfig.Tests
{
    [TestFixture]
    public class CompositeFilterTests
    {
        [Test]
        public void Filter_WhenSeveralNegateValuesFromDiffrentSubject_GetRelevantValue()
        {
            // The uniquness of the context is A+B, I would like to filter both of them and only one
            var context = new Dictionary<string, string>
                              {
                                  {"A", "1"}, 
                                  {"B", "3"},
                              };

            var items = new[]
                            {
                                new Item(1,new TextMatchCondition("A","1",true),new TextMatchCondition("B","2",true))
                            };

            var evaluationResult = new FilterConditionsEvaluator().Evaluate(context, items);
            var result = new BestMatchFilterPolicy().Filter(evaluationResult).Select(x => x.Item).OfType<Item>();
            
            CollectionAssert.IsNotEmpty(result.ToList());
        }
    }
}
