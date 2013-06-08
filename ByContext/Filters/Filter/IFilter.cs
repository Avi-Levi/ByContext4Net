using System.Collections.Generic;

namespace ByContext.Filters.Filter
{
    public interface IFilter
    {
        IHaveFilterConditions[] FilterItems(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items);
    }
}