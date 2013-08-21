using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ByContext.Filters.Filter;
using ByContext.Logging;
using ByContext.ResultBuilder;
using ByContext.ValueProviders;

namespace ByContext.ParameterValueProviders
{
    public class ParameterValueProvider : IParameterValueProvider
    {
        private const string TimerLogText = "Filtering and building values duration: ";

        public ParameterValueProvider(IValueProvider[] items, IResultBuilder resultBuilder, IFilter filter, bool required, string name, ILoggerProvider loggerProvider)
        {
            this.Items = items;
            this.ResultBuilder = resultBuilder;
            this.Filter = filter;
            this.Required = required;
            this.Name = name;
            this.TimerLogger = loggerProvider.GetLogger(LogHeirarchy.Root.Timer.Value, GetType());
            this.FlowLogger = loggerProvider.GetLogger(LogHeirarchy.Root.Flow.Value, GetType());
        }

        private ILogger TimerLogger { get; set; }
        private ILogger FlowLogger { get; set; }
        private string Name { get; set; }
        private IValueProvider[] Items { get; set; }
        private IResultBuilder ResultBuilder { get; set; }
        private IFilter Filter { get; set; }

        private bool Required { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            this.FlowLogger.Debug(() => string.Format("ParameterValueProvider.Get for parameter with name: {0} and context: {1}", this.Name, runtimeContext.FormatString()));
            this.FlowLogger.Debug(() => this.LogInputParameters(this.Items));

            var watch = Stopwatch.StartNew();

            object[] valuesByPolicy = this.Filter.FilterItems(runtimeContext, this.Items).OfType<IValueProvider>().Select(v => v.Get()).ToArray();

            watch.Stop();

            this.TimerLogger.Debug(TimerLogText + watch.ElapsedMilliseconds);

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
