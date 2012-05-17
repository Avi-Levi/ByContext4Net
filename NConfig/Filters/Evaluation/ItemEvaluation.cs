namespace NConfig.Filters.Evaluation
{
    public struct ItemEvaluation
    {
        public ItemEvaluation(IHaveFilterConditions item, ConditionEvaluation[] conditionsEval)
        {
            Item = item;
            ConditionsEvaluation = conditionsEval;
        }

        public readonly IHaveFilterConditions Item;
        public readonly ConditionEvaluation[] ConditionsEvaluation;
    }
}
