using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public static class Extensions
    {
        public static string FormatString(this IDictionary<string, string> source)
        {
            const string keyValueSeperator = ":";
            const string itemsSeperator = "\\";

            if (source != null && source.Any())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in source)
                {
                    sb.Append(item.Key);
                    sb.Append(keyValueSeperator);
                    sb.Append(item.Value);
                    sb.Append(itemsSeperator);
                }

                return sb.ToString();
            }
            else
            {
                return "Empty";
            }
        }

        public static string FormatString(this KeyValuePair<string, string> source)
        {
            return source.Key + "\\" + source.Value;
        }

        public static string FormatString(this IEnumerable<ParameterValue> source)
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
                        sb.AppendLine(reference.SubjectName + "-" + reference.SubjectValue);
                    }
                }

                return sb.ToString();
            }
            else
            {
                return "Empty";
            }
        }

        public static IEnumerable<ParameterValue> Filter(this IEnumerable<ParameterValue> source, KeyValuePair<string, string> contextItem)
        {
            var query = from item in source
                        where item.References.Any(x => x.SubjectName == contextItem.Key && x.SubjectValue == contextItem.Value)
                        select item;

            return query;
        }

        public static bool Any(this IEnumerable<ContextSubjectReference> source, string subjectName)
        {
            return source.Any(x => x.SubjectName == subjectName && x.SubjectValue == ContextSubjectReference.ALL);
        }

        public static bool Any(this IEnumerable<ContextSubjectReference> source, KeyValuePair<string, string> contextItem)
        {
            return source.Any(x => x.SubjectName == contextItem.Key && x.SubjectValue == contextItem.Value);
        }

        public static IEnumerable<ParameterValue> Filter(this IEnumerable<ParameterValue> source, string subjectName)
        {
            var query = from item in source
                        where item.References.Any(x => x.SubjectName == subjectName && x.SubjectValue == ContextSubjectReference.ALL)
                        select item;

            return query;
        }

        public static bool Any(this IEnumerable<ParameterValue> source, KeyValuePair<string, string> contextItem)
        {
            return source.Filter(contextItem).Any();
        }
        public static bool Any(this IEnumerable<ParameterValue> source, string subjectName)
        {
            return source.Filter(subjectName).Any();
        }
    }
}
