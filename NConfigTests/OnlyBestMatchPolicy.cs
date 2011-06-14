using NConfig;
using NConfig.Impl;
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
