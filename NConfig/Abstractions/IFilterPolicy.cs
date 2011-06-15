using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public interface IFilterPolicy
    {
        IList<IFilterRule> Rules { get; }

        IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items,
            IDictionary<string, string> runtimeContext);
    }
}
