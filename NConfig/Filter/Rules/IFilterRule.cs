using System.Collections.Generic;

using NConfig.Configuration;
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
