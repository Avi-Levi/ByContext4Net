using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig
{
    public interface IRuntimeContextProvider
    {
        IDictionary<string, string> Get();
    }
}
