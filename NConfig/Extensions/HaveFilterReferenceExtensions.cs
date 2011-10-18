using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public static class HaveFilterReferenceExtensions
    {
        public static IEnumerable<IHaveFilterReference> SelectItemsWithSpecificReference(this IEnumerable<IHaveFilterReference> source, KeyValuePair<string, string> contextItem)
        {
            var query = from item in source
                        where item.References.Any(x => x.Name == contextItem.Key && x.Value == contextItem.Value)
                        select item;

            return query;
        }
        
        public static IEnumerable<IHaveFilterReference> SelectItemsWithNoSpecificReferenceToOtherSubjectValue
            (this IEnumerable<IHaveFilterReference> source, KeyValuePair<string, string> contextItem)
        {
            var query = from item in source
                        where !item.References.Any(x => x.Name == contextItem.Key && x.Value != contextItem.Value)
                        select item;

            return query;
        }

        public static bool AnySpecificReference(this IEnumerable<IHaveFilterReference> source, KeyValuePair<string, string> contextItem)
        {
            return source.SelectItemsWithSpecificReference(contextItem).Any();
        }
    }
}
