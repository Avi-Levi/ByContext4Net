using System;
using NConfig.Model;

namespace NConfig.Exceptions
{
    public class SessionToProviderConvertionFailed : NConfigException
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