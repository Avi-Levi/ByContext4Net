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

using System.Collections.Generic;
using ByContext.FilterConditions;

namespace ByContext.Query
{
    public class FilterConditionsPerSubjectQueryContributor : IQueryContributor
    {
        public IFilterCondition[] Conditions { get; set; }
        public string Subject { get; set; }
        public void Handle(IDictionary<string, string> context, IProbe probe)
        {
            probe.ReferencesCount++;
            if (context.ContainsKey(this.Subject))
            {
                probe.Trace("{0} - subject: {1} evaluating...", this.GetType().Name, this.Subject);
                int explicitNoneNegatingNegativeCount = 0;
                bool hasExplicitPositiveReferencesForCurrentSubject = false;
                foreach (var condition in Conditions)
                {
                    var b = condition.Evaluate(context[this.Subject]);
                    probe.Trace("{0} - condidtion {1} evaluated to {2}", this.GetType().Name, condition.GetType().Name, b);
                    // if condition matched
                    if (b)
                    {
                        if (condition.Negate)
                        {
                            probe.Trace("{0} excluded value because condidtion {1} evaluated to true and was negated", this.GetType().Name, condition.GetType().Name, b);
                            probe.Exclude = true;
                            break;
                        }
                        else
                        {
                            probe.ExplicitPositiveReferencesCount++;
                            hasExplicitPositiveReferencesForCurrentSubject = true;
                        }
                    }
                    // if condition did not match
                    else
                    {
                        if (condition.Negate)
                        {
                            probe.Trace("{0} - added explicit positive reference, because condidtion {1} evaluated to false and was negated", this.GetType().Name, condition.GetType().Name, b);
                            probe.ExplicitPositiveReferencesCount++;
                            hasExplicitPositiveReferencesForCurrentSubject = true;
                        }
                        else
                        {
                            explicitNoneNegatingNegativeCount++;
                        }
                    }
                }
                if (explicitNoneNegatingNegativeCount > 0 && !hasExplicitPositiveReferencesForCurrentSubject)
                {
                    probe.Trace("excluded value bacause it has no explicit positive references count and {0} explicit none nagating negative references", explicitNoneNegatingNegativeCount);
                    probe.Exclude = true;
                }
            }
        }
    }
}