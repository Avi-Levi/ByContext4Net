using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public class HasSpecificSubjectReferenceRule : IFilterRule
    {
        public string Description { get { return "the item must have a specific reference to the subject."; } }

        public bool IsSatisfied(IEnumerable<ContextSubjectReference> references,
            KeyValuePair<string, string> runtimeSubjectContext)
        {
            return (
                references.Any(x => 
                    x.SubjectName.Equals(runtimeSubjectContext.Key)
                    && 
                    x.SubjectValue.Equals(runtimeSubjectContext.Value))
                );
        }
    }
}
