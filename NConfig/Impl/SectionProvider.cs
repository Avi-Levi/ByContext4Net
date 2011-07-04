using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;
using NConfig.Model;

namespace NConfig.Impl
{
    public class SectionProvider : ISectionProvider
    {
        public SectionProvider(Type sectionType)
        {
            this.ParameterValuesProviders = new Dictionary<string, IValueProvider>();
            this.SectionType = sectionType;
        }

        private Type SectionType { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<string, IValueProvider> ParameterValuesProviders { get; private set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();

            foreach (var provider in this.ParameterValuesProviders)
            {
                values.Add(provider.Key,provider.Value.Get(runtimeContext));
            }

            return this.ModelBinder.Bind(this.SectionType, values);
        }
    }
}
