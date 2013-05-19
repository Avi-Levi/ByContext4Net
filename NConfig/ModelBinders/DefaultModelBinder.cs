using System.Collections.Generic;
using System.Reflection;

namespace NConfig.ModelBinders
{
    public class DefaultModelBinder : IModelBinder
    {
        private readonly IDictionary<string, PropertyInfo> _propertyInfos;

        public DefaultModelBinder(IDictionary<string, PropertyInfo> propertyInfos)
        {
            _propertyInfos = propertyInfos;
        }

        public void Bind(object instance, IDictionary<string, object> parametersInfo)
        {
            foreach (var param in parametersInfo)
            {
                this._propertyInfos[param.Key].SetValue(instance, param.Value, null);
            }
        }
    }
}
