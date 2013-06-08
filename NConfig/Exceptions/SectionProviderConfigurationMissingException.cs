using System;

namespace ByContext.Exceptions
{
    public class SectionProviderConfigurationMissingException : ByContextException
    {
        public Type SectionType { get; private set; }

        public SectionProviderConfigurationMissingException(Type sectionType)
            : base(string.Format("Provider for section: {0} is missing, make sure configuration data exists for the given section", sectionType.FullName))
        {
            SectionType = sectionType;
        }
    }
}