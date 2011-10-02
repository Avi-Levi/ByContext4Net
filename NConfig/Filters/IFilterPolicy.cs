using System.Collections.Generic;

namespace NConfig.Filters
{
    public interface IFilterPolicy
    {
        IEnumerable<IHaveFilterReference> Filter(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterReference> items);
    }
}
