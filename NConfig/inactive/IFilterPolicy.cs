using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Policy
{
    public interface IFilterPolicy
    {

        bool IsSatisfied(IList<Model.ContextSubjectReference> references, 
            KeyValuePair<string, string> runtimeContextItem, out string notSatisfiedReasone);
    }
}
