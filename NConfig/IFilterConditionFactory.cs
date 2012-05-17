using System.Collections.Generic;
using NConfig.Filters.Conditions;

namespace NConfig
{
    public interface IFilterConditionFactory
    {
        IFilterCondition Create(Dictionary<string, string> properties);
    }
}