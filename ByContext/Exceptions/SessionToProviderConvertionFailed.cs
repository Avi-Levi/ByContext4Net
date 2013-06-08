using System;
using ByContext.Model;

namespace ByContext.Exceptions
{
    public class SessionToProviderConvertionFailed : ByContextException
    {
        public Section Section { get; private set; }

        public SessionToProviderConvertionFailed(Section section, Exception inner)
            : base(
                string.Format("Failed converting section {0}, see inner exception for more detailes.", section.TypeName),
                inner)
        {
            Section = section;
        }
    }
}