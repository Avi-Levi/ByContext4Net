using System;
using System.Collections.Generic;
using System.Linq;
using ByContext.ModelBinders;
using ByContext.ParameterValueProviders;

namespace ByContext.SectionProviders
{
    public class SectionProvider : ISectionProvider
    {
        public SectionProvider()
        {
            this.ParameterValuesProviders = new Dictionary<string, IParameterValueProvider>();
        }

        public Type SectionType { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IDictionary<string, IParameterValueProvider> ParameterValuesProviders { get; private set; }

        public object Get(IDictionary<string, string> runtimeContext)
        {
            var values = this.ParameterValuesProviders
                             .Select(x => new { name = x.Key, value = x.Value.Get(runtimeContext) })
                             .Where(x => x.value != null)
                             .ToDictionary(x => x.name, x => x.value);;

            object instance = Activator.CreateInstance(this.SectionType, true);

            this.ModelBinder.Bind(instance, values);

            return instance;
        }
    }
}
