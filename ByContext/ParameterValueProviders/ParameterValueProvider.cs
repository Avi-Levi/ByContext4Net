using System;
using System.Collections.Generic;
using System.Linq;
using ByContext.Filters.Filter;
using ByContext.ResultBuilder;
using ByContext.ValueProviders;
using ByContext.Extensions;

namespace ByContext.ParameterValueProviders
{
    public class ParameterValueProvider : IParameterValueProvider
    {
        public ParameterValueProvider(IEnumerable<IValueProvider> items,IResultBuilder resultBuilder, IFilter filter, bool required, string name)
        {
            this.Items = items;
            this.ResultBuilder = resultBuilder;
            this.Filter = filter;
            this.Required = required;
            this.Name = name;
        }

        private string Name{ get; set; }
        private IEnumerable<IValueProvider> Items { get; set; }
        private IResultBuilder ResultBuilder { get; set; }
        private IFilter Filter { get; set; }

        private bool Required { get; set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            object[] valuesByPolicy = this.Filter.FilterItems(runtimeContext,this.Items).OfType<IValueProvider>().Select(v => v.Get()).ToArray();

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
