using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Policy;

namespace NConfig
{
    /// <summary>
    /// Represents data item, which we want to have a different value
    /// according to the context provided at runtime by the caller.
    /// </summary>
    public class Parameter
    {
        public IList<IFilterPolicy> Policies { get; set; }
    }
}
