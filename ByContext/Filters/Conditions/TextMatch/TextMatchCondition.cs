// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
