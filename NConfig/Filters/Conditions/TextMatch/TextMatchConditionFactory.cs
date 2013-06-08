using System.Collections.Generic;

namespace ByContext.Filters.Conditions.TextMatch
{
    public class TextMatchConditionFactory : IFilterConditionFactory
    {
        public IFilterCondition Create(Dictionary<string, string> properties)
        {
            var subject = properties["Subject"];
            var value = properties["Value"];
            var negate = properties.ContainsKey("Negate") ? bool.Parse(properties["Negate"]) : false;
            return new TextMatchCondition(subject,value,negate);
        }
    }
}