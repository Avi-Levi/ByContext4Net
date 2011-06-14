using System.Collections.Generic;

using NConfig.Model;

namespace NConfig
{
    public interface IFilterRule
    {
        IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, IDictionary<string, string> runtimeContext,
            KeyValuePair<string, string> currentRuntimeContextItem);
    }
}
