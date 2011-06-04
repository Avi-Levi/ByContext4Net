using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig.Rules
{
    public interface IFilterRule
    {
        string Description { get; }
        bool IsSatisfied(IEnumerable<ContextSubjectReference> references, KeyValuePair<string,string> runtimeSubjectContext);
    }
}
