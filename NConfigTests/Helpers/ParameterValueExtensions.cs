using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Configuration;
using NConfig.Abstractions;

namespace NConfig.Tests
{
    public static class ParameterValueExtensions
    {
        public static ParameterValue WithReference(this ParameterValue source, string subjectName, string subjectValue)
        {
            source.References.Add(ContextSubjectReference.Create(subjectName, subjectValue));
            return source;
        }
        public static ParameterValue WithAllReferenceToSubject(this ParameterValue source, string subjectName)
        {
            source.References.Add(ContextSubjectReference.Create(subjectName, ContextSubjectReference.ALL));
            return source;
        }
    }
}
