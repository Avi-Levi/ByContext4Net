using System.Collections.Generic;

namespace NConfig.ModelBinders
{
    /// <summary>
    /// Binds configuration parameters values to a given section instance.
    /// </summary>
    public interface IModelBinder
    {
        void Bind(object instance, IDictionary<string, object> parametersInfo);
    }
}
