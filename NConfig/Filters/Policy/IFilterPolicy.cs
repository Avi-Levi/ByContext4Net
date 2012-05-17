using System.Collections.Generic;
using NConfig.Filters.Evaluation;

namespace NConfig.Filters.Policy
{
    public interface IFilterPolicy
    {
        IEnumerable<ItemEvaluation> Filter(IEnumerable<ItemEvaluation> evaluatedItems);
    }
}
