using System.Collections.Generic;
using NConfig.Abstractions;

namespace NConfig.Filter.Rules
{
    /// <summary>
    /// if the source collection has items with a specific reference to the given <paramref name="runtimeContextItemToFilterBy"/>
    /// then selects them.
    /// else - selects the items with an "ALL" reference to the given <paramref name="runtimeContextItemToFilterBy"/>.
    /// </summary>
    public class BestMatchRule : IFilterRule
    {
        /// <summary>
        /// if <paramref name="items"/> has items with a specific reference to <paramref name="runtimeContextItemToFilterBy"/>, 
        /// selects them.
        /// else - selects the items with an all reference to <paramref name="runtimeContextItemToFilterBy"/>.
        /// </summary>
        public IEnumerable<IHaveFilterReference> Apply(
            IEnumerable<IHaveFilterReference> items,
            IDictionary<string, string> runtimeContext,
            KeyValuePair<string, string> runtimeContextItemToFilterBy)
        {
            if (items.AnySpecificReference(runtimeContextItemToFilterBy))
            {
                return items.SelectItemsWithSpecificReference(runtimeContextItemToFilterBy);
            }
            else
            {
                return items.SelectItemsWithAllReference(runtimeContextItemToFilterBy.Key);
            }
        }
    }
}
