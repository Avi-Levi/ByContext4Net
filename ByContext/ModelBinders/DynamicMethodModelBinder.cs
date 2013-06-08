using System;
using System.Collections.Generic;

namespace ByContext.ModelBinders
{
    public class DynamicMethodModelBinder : IModelBinder
    {
        private readonly Dictionary<string, Action<object, object>> _injectors;

        public DynamicMethodModelBinder(Dictionary<string, Action<object, object>> injectors)
        {
            _injectors = injectors;
        }

        public void Bind(object instance, IDictionary<string, object> parametersInfo)
        {
            foreach (var pi in parametersInfo)
            {
                this._injectors[pi.Key](instance, pi.Value);
            }
        }
    }
}