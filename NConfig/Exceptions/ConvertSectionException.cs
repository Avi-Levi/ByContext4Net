using System;
using NConfig.Model;

namespace NConfig.Exceptions
{
    public class ConvertSectionException : NConfigException
    {
        public Section Section { get; private set; }

        public ConvertSectionException(Section section, Exception exception) : base(string.Format("Failed converting section {0}, see inner exception for more details",section.TypeName), exception)
        {
            Section = section;
        }
    }
}