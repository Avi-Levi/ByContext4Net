using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Exceptions
{
    public class GetSectionException : ApplicationException
    {
        public GetSectionException(Type sectionType, Exception inner): 
            base(string.Format("Failed providing section: {0}, see inner exception for more detailes.", sectionType.FullName))
        {
            this.SectionName = sectionType.FullName;
        }

        public string SectionName { get; private set; }
    }
}
