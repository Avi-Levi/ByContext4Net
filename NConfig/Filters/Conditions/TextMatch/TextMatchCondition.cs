using ByContext.Filters.Evaluation;

namespace ByContext.Filters.Conditions.TextMatch
{
    public class TextMatchCondition : IFilterCondition
    {
        public static readonly string Name = "TextMatch";

        public string Subject { get; private set; }
        public string Value { get; set; }
        public bool Negate { get; private set; }

        public override string ToString()
        {
            return this.Subject + " " + this.Value + " " + this.Negate.ToString();
        }
        public TextMatchCondition(string subject, string value):this(subject,value,false)
        {}

        public TextMatchCondition(string subject, string value, bool negate)
        {
            Subject = subject;
            Value = value;
            Negate = negate;
        }

        public bool Evaluate(ConditionEvaluationContext context)
        {
            return context.CurrentRuntimeContextItem.Key == Subject && context.CurrentRuntimeContextItem.Value == Value;
        }
    }
}
