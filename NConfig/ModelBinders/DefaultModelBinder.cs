using System;
using System.Collections.Generic;
using System.Reflection;

namespace NConfig.ModelBinders
{
    public class DefaultModelBinder : IModelBinder
    {
        public object Bind(Type modelType, IDictionary<string, object> parametersInfo)
        {
            object result = Activator.CreateInstance(modelType);

            foreach (var param in parametersInfo)
            {
                var paramPI = modelType.GetProperty(param.Key, BindingFlags.Public | BindingFlags.Instance);
                paramPI.SetValue(result, param.Value, null);
            }

            return result;
        }
    }
}
