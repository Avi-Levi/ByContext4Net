using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Filter.Rules
{
    public class WithSpecificOrNoReferenceToSubjectRule : IFilterRule
    {
        /// <summary>
        /// if <paramref name="items"/> has items with a specific reference to <paramref name="runtimeContextItemToFilterBy"/>, 
        /// selects them.
        /// else - selects all items.
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
                return items.SelectItemsWithNoSpecificReferenceToOtherSubjectValue(runtimeContextItemToFilterBy);
            }
        }
    }
}
