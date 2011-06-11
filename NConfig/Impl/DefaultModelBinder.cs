using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NConfig.Impl
{
    public class DefaultModelBinder : IModelBinder
    {
        public object Bind(Type modelType, IEnumerable<Model.Parameter> parameters)
        {
            object result = Activator.CreateInstance(modelType);
            foreach (var parameter in parameters)
            {
                PropertyInfo pi = modelType.GetProperty(parameter.Name);
                object value = parameter.Parse(parameter.Values.Select(v => v.Value));
                pi.SetValue(result, value, null);
            }


            return result;
        }
    }
}
