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
using System.Diagnostics;
using System.Linq;
using ByContext.Filters.Evaluation;
using ByContext.Filters.Policy;
using ByContext.Logging;

namespace ByContext.Filters.Filter
{
    public class Filter : IFilter
    {
        private const string FilteritemsText = "Filter items duration: ";

        public Filter(IFilterPolicy policy, IFilterConditionsEvaluator filterConditionsEvaluator, ILoggerProvider loggerProvider)
        {
            this.Policy = policy;
            this.FilterConditionsEvaluator = filterConditionsEvaluator;
            this.TimerLogger = loggerProvider.GetLogger(LogHeirarchy.Root.Timer.Value, GetType());
        }

        private IFilterPolicy Policy { get; set; }
        private IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        private ILogger TimerLogger { get; set; }

        public IHaveFilterConditions[] FilterItems(IDictionary<string, string> runtimeContext, IEnumerable<IHaveFilterConditions> items)
        {
            var watch = Stopwatch.StartNew();

            IEnumerable<ItemEvaluation> itemsEvaluationResult = this.FilterConditionsEvaluator.Evaluate(runtimeContext, items);

            var result = this.Policy.Filter(itemsEvaluationResult).Select(x => x.Item).ToArray();

            watch.Stop();

            this.TimerLogger.Debug(FilteritemsText + watch.ElapsedMilliseconds);

            return result;
        }

    }
}