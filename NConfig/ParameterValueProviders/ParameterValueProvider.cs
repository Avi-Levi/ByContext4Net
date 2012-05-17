using System;
using System.Collections.Generic;
using System.Linq;
using NConfig.Extensions;
using NConfig.Filters;
using NConfig.Filters.Evaluation;
using NConfig.Filters.Policy;
using NConfig.ResultBuilder;
using NConfig.ValueProviders;

namespace NConfig.ParameterValueProviders
{
    public class ParameterValueProvider : IParameterValueProvider
    {
        public ParameterValueProvider(IEnumerable<IValueProvider> items,IFilterPolicy policy, IResultBuilder resultBuilder, IFilterConditionsEvaluator filterConditionsEvaluator,
            bool required, string name)
        {
            this.Items = items;
            this.Policy = policy;
            this.ResultBuilder = resultBuilder;
            this.Required = required;
            this.Name = name;
            this.FilterConditionsEvaluator = filterConditionsEvaluator;
        }

        private string Name{ get; set; }
        private IEnumerable<IValueProvider> Items { get; set; }
        private IFilterPolicy Policy { get; set; }
        private IResultBuilder ResultBuilder { get; set; }
        private IFilterConditionsEvaluator FilterConditionsEvaluator { get; set; }
        
        private bool Required { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            IEnumerable<ItemEvaluation> itemsEvaluationResult = this.FilterConditionsEvaluator.Evaluate(runtimeContext, this.Items);

            object[] valuesByPolicy = this.Policy.Filter(itemsEvaluationResult).Select(x=>x.Item).OfType<IValueProvider>().Select(v => v.Get()).ToArray();

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
    }
}
