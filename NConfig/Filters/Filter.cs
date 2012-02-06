using System;
using System.Collections.Generic;

namespace NConfig.Filters
{
    /// <summary>
    /// filteres items according to relevance
    /// </summary>
    public class FilterPolicy2
    {
        public IEnumerable<IHaveFilterReference> Filter(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterReference> items)
        {
            throw new NotImplementedException();
        }
    }
}
