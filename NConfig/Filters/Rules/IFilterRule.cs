using System.Collections.Generic;

namespace NConfig.Filters.Rules
{
    /// <summary>
    /// Filter items according to a given runtime context and current context item.
    /// </summary>
    public interface IFilterRule
    {
        IEnumerable<IHaveFilterReference> Apply
            (
            IEnumerable<IHaveFilterReference> items, 
            IDictionary<string, string> runtimeContext, 
            KeyValuePair<string, string> currentRuntimeContextItem
            );
    }
}
