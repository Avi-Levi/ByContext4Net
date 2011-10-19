using System.Collections.Generic;

namespace NConfig.Filters
{
    /// <summary>
    /// Used to filter items according to the runtime context.
    /// </summary>
    public interface IFilterPolicy
    {
        /// <summary>
        /// Filters the given <paramref name="items"/> by relevance to the given <paramref name="runtimeContext"/>.
        /// </summary>
        IEnumerable<IHaveFilterReference> Filter(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterReference> items);
    }
}
