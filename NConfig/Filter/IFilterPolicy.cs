using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig
{
    public interface IFilterPolicy
    {
        IEnumerable<IHaveFilterReference> Filter(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterReference> items);
    }
}
