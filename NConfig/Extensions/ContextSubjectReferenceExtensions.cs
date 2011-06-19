using System.Collections.Generic;
using System.Linq;
using NConfig.Model;

namespace NConfig
{
    public static class ContextSubjectReferenceExtensions
    {
        public static bool Any(this IEnumerable<ContextSubjectReference> source, string subjectName)
        {
            return source.Any(x => x.SubjectName == subjectName && x.SubjectValue == ContextSubjectReference.ALL);
        }

        public static bool Any(this IEnumerable<ContextSubjectReference> source, KeyValuePair<string, string> contextItem)
        {
            return source.Any(x => x.SubjectName == contextItem.Key && x.SubjectValue == contextItem.Value);
        }
    }
}
