using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public interface IFilterRule
    {
        IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items, IDictionary<string, string> runtimeContext,
            KeyValuePair<string, string> currentRuntimeContextItem);
    }
}
