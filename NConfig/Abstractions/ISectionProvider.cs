using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Abstractions
{
    public interface ISectionProvider
    {
        object Get(IDictionary<string, string> runtimeContext);
    }
}
