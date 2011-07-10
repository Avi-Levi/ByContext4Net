using System.Collections.Generic;
using System.Linq;
using NConfig.Model;

namespace NConfig
{
    public static class ContextSubjectReferenceExtensions
    {
        public static bool HasAllReference(this IEnumerable<ContextSubjectReference> source, string subjectName)
        {
            return source.Any(x => x.Name == subjectName && x.Value == ContextSubjectReference.ALL);
        }

        public static bool HasSpecificReference(this IEnumerable<ContextSubjectReference> source, KeyValuePair<string, string> contextItem)
        {
            return source.Any(x => x.Name == contextItem.Key && x.Value == contextItem.Value);
        }
    }
}
