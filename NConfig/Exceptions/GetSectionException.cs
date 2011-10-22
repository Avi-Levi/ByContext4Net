using System;

namespace NConfig.Exceptions
{
    /// <summary>
    /// Thrown by the <see cref="ConfigurationService"/> when an <see cref="ConfigurationService.GetSection"/> failed.
    /// </summary>
    public class GetSectionException : NConfigException
    {
        public GetSectionException(Type sectionType, Exception inner): 
            base(string.Format("Failed providing section: {0}, see inner exception for more detailes.", sectionType.FullName), inner)
        {
            this.SectionName = sectionType.FullName;
        }

        public string SectionName { get; private set; }
    }
}
