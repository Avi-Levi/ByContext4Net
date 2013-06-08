using System.Collections.Generic;

namespace ByContext.Filters.Evaluation
{
    public interface IFilterConditionsEvaluator
    {
        IEnumerable<ItemEvaluation> Evaluate(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items);
    }
}
