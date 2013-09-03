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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ByContext.Logging;
using ByContext.Query.QueryEngine;
using ByContext.ResultBuilder;

namespace ByContext.ParameterValueProviders
{
    public class QueryEngineParameterValueProvider : IParameterValueProvider
    {
        private readonly IQueryEngine _queryEngine;
        private const string TimerLogText = "Filtering and building values duration: ";

        public QueryEngineParameterValueProvider(IQueryEngine queryEngine, IResultBuilder resultBuilder, bool required, string name, ILoggerProvider loggerProvider)
        {
            _queryEngine = queryEngine;
            ResultBuilder = resultBuilder;
            Required = required;
            Name = name;
            TimerLogger = loggerProvider.GetLogger(LogHeirarchy.Root.Timer.Value, GetType());
            FlowLogger = loggerProvider.GetLogger(LogHeirarchy.Root.Flow.Value, GetType());
        }

        private ILogger TimerLogger { get; set; }
        private ILogger FlowLogger { get; set; }
        private string Name { get; set; }
        private IResultBuilder ResultBuilder { get; set; }

        private bool Required { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            object[] valuesByPolicy = this._queryEngine.Query(runtimeContext).Select(p=>p.Get()).ToArray();

            if (!valuesByPolicy.Any())
            {
                if (this.Required)
                {
                    throw new InvalidOperationException(string.Format(
                        "no values selected for parameter {0} using context {1}", this.Name, runtimeContext.FormatString()));
                }
                else
                {
                    return null;
                }
            }

            return this.ResultBuilder.Build(valuesByPolicy);
        }

        private string LogInputParameters(IEnumerable<IHaveFilterConditions> items)
        {
            var sb = new StringBuilder();

            sb.Append("filtered items: " + Environment.NewLine);

            foreach (var item in items)
            {
                sb.Append("item: " + item.ToString() + Environment.NewLine);
                sb.Append("Conditions:" + Environment.NewLine);
                item.FilterConditions.ForEach(condition => sb.Append(condition.ToString() + " | "));
            }

            return sb.ToString();
        }

    }
}
