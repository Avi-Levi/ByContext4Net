using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public static class ParameterValueExtensions
    {
        public static ParameterValue WithReference(this ParameterValue source, string subjectName, string subjectValue)
        {
            source.References.Add(new ContextSubjectReference{SubjectName = subjectName,SubjectValue = subjectValue});
            return source;
        }
    }
}
