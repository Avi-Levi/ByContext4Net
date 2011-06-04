using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public class HaveAllReferenceRule : IFilterRule
    {
        public string Description { get{return "must have an 'ALL' refernce to subject.";} }

        public bool IsSatisfied(IEnumerable<ContextSubjectReference> references,
            KeyValuePair<string, string> runtimeSubjectContext)
        {
            return (references.Any
                (x => 
                    x.SubjectName.Equals(runtimeSubjectContext.Key)
                    &&
                    x.SubjectValue.Equals(ContextSubjectReference.ALL))
                );
        }
    }
}
