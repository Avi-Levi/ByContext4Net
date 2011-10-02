using System;
using System.Collections.Generic;
using System.Linq;
using NConfig.Extensions;
using NConfig.Filters;
using NConfig.ResultBuilder;

namespace NConfig.ParameterValueProviders
{
    public class ParameterValueProvider : IParameterValueProvider
    {
        public ParameterValueProvider(IEnumerable<IValueProvider> items,IFilterPolicy policy, IResultBuilder resultBuilder, 
            bool required, string name)
        {
            this.Items = items;
            this.Policy = policy;
            this.ResultBuilder = resultBuilder;
            this.Required = required;
            this.Name = name;
        }

        private string Name{ get; set; }
        private IEnumerable<IValueProvider> Items { get; set; }
        private IFilterPolicy Policy { get; set; }
        private IResultBuilder ResultBuilder { get; set; }
        
        private bool Required { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            object[] valuesByPolicy = this.Policy.Filter(runtimeContext, this.Items).OfType<IValueProvider>().Select(v => v.Get()).ToArray();

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
