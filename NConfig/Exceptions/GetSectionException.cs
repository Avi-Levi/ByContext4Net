using System;

namespace ByContext.Exceptions
{
    /// <summary>
    /// Thrown by the <see cref="ByContext"/> when an <see cref="ByContext.GetSection"/> failed.
    /// </summary>
    public class GetSectionException : ByContextException
    {
        public GetSectionException(Type sectionType, Exception inner): 
            base(string.Format("Failed providing section: {0}, see inner exception for more detailes.", sectionType.FullName), inner)
        {
            this.SectionName = sectionType.FullName;
        }

        public string SectionName { get; private set; }
    }
}
