using System.Collections.Generic;
using ByContext.Filters.Evaluation;

namespace ByContext.Filters.Policy
{
    public interface IFilterPolicy
    {
        ItemEvaluation[] Filter(IEnumerable<ItemEvaluation> evaluatedItems);
    }
}
