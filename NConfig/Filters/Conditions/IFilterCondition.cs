using ByContext.Filters.Evaluation;

namespace ByContext.Filters.Conditions
{
    /// <summary>
    /// represents a filter condition.
    /// </summary>
    public interface IFilterCondition
    {
        /// <summary>
        /// When implemented by a derived class, evaluates the condition.
        /// </summary>
        bool Evaluate(ConditionEvaluationContext context);

        string Subject { get; }
        bool Negate { get;}
    }
}