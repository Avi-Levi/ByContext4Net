using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NConfig.Impl
{
    public class DefaultModelBinder : IModelBinder
    {
        public object Bind(Type modelType, IDictionary<string, object> parametersInfo)
        {
            object result = Activator.CreateInstance(modelType);

            foreach (var pi in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                pi.SetValue(result, parametersInfo[pi.Name], null);
            }

            return result;
        }
    }
}
