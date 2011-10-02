using System.Collections.Generic;

namespace NConfig.Filters.Rules
{
    /// <summary>
    /// interface for fiter rules 
    /// </summary>
    public interface IFilterRule
    {
        IEnumerable<IHaveFilterReference> Apply(IEnumerable<IHaveFilterReference> items, IDictionary<string, string> runtimeContext, 
            KeyValuePair<string, string> currentRuntimeContextItem);
    }
}
