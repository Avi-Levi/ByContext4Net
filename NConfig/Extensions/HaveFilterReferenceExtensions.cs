using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.Abstractions;

namespace NConfig
{
    public static class HaveFilterReferenceExtensions
    {
        public static IEnumerable<IHaveFilterReference> SelectItemsWithSpecificReference(this IEnumerable<IHaveFilterReference> source, KeyValuePair<string, string> contextItem)
        {
            var query = from item in source
                        where item.References.HasSpecificReference(contextItem)
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
        

        public static IEnumerable<IHaveFilterReference> SelectItemsWithAllReference(this IEnumerable<IHaveFilterReference> source, string subjectName)
        {
            var query = from item in source
                        where item.References.HasAllReference(subjectName)
                        select item;

            return query;
        }

        public static bool AnySpecificReference(this IEnumerable<IHaveFilterReference> source, KeyValuePair<string, string> contextItem)
        {
            return source.SelectItemsWithSpecificReference(contextItem).Any();
        }

        public static bool AnyItemsWithAllReference(this IEnumerable<IHaveFilterReference> source, string subjectName)
        {
            return source.SelectItemsWithAllReference(subjectName).Any();
        }

        public static string FormatString(this IEnumerable<IHaveFilterReference> source)
        {
            if (source != null && source.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in source)
                {
                    sb.AppendLine();
                    sb.AppendLine(item.ToString());

                    foreach (var reference in item.References)
                    {
                        sb.AppendLine(reference.Name + "-" + reference.Value);
                    }
                }

                return sb.ToString();
            }
            else
            {
                return "Empty";
            }
        }
    }
}
