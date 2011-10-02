using System;

namespace NConfig
{
    public interface IConfigurationService
    {
        TSection GetSection<TSection>() where TSection : class;
        object GetSection(Type sectionType);
        TSection GetSection<TSection>(Type sectionType) where TSection : class;
        IConfigurationService WithReference(string subjectName, string subjectValue);
    }
}
