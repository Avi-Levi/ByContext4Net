using System.Collections.Generic;

using NConfig.Model;
using NConfig.Abstractions;

namespace NConfig.Filter.Rules
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
