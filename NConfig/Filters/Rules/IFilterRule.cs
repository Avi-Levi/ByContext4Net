using System.Collections.Generic;

namespace NConfig.Filters.Rules
{
    /// <summary>
    /// Used to filter items according to the runtime context and the current context item.
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
