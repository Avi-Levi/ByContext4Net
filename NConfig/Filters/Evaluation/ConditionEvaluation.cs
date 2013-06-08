using ByContext.Filters.Conditions;

namespace ByContext.Filters.Evaluation
{
    public struct ConditionEvaluation
    {
        public readonly RelationToContextEnum RelationToContext;
        public readonly ConditionEvaluationContext Context;
        public readonly IFilterCondition Condition;

        public ConditionEvaluation(RelationToContextEnum relationToContext, ConditionEvaluationContext context, IFilterCondition condition)
        {
            RelationToContext = relationToContext;
            Context = context;
            Condition = condition;
        }
    }
}