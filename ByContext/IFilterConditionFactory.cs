using System.Collections.Generic;
using ByContext.Filters.Conditions;

namespace ByContext
{
    public interface IFilterConditionFactory
    {
        IFilterCondition Create(Dictionary<string, string> properties);
    }
}