using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Policy
{
    public interface IFilterPolicy
    {
        IEnumerable<ParameterValue> Apply(IEnumerable<ParameterValue> items,
            IDictionary<string, string> runtimeContext);
    }
}
