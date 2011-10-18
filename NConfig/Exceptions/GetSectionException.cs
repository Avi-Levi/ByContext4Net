using System;

namespace NConfig.Exceptions
{
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
