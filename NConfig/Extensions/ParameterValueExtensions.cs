using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public static class ParameterValueExtensions
    {
        public static ParameterValue WithReference(this ParameterValue source, string subjectName, string subjectValue)
        {
            source.References.Add(new ContextSubjectReference{SubjectName = subjectName,SubjectValue = subjectValue});
            return source;
        }
        public static IEnumerable<ParameterValue> Filter(this IEnumerable<ParameterValue> source, KeyValuePair<string, string> contextItem)
        {
            var query = from item in source
                        where item.References.Any(x => x.SubjectName == contextItem.Key && x.SubjectValue == contextItem.Value)
                        select item;

            return query;
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
    }
}
