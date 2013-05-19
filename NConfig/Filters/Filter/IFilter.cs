using System.Collections.Generic;

namespace NConfig.Filters.Filter
{
    public interface IFilter
    {
        IHaveFilterConditions[] FilterItems(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items);
    }
}