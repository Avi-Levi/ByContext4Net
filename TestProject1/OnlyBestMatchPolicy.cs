using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Policy;
using NConfig.Model;
using NConfig.Rules;

namespace TestProject1
{
    class OnlyBestMatchPolicy : FilterPolicy
    {
        public OnlyBestMatchPolicy():base(
            new IFilterRule[2]{new WithSpecificOrALLRerefenceToSubjectRule{},new BestMatchRule()})
        {}
    }
}
