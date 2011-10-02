using System;
using System.Collections.Generic;

namespace NConfig.ModelBinders
{
    public interface IModelBinder
    {
        object Bind(Type modelType, IDictionary<string, object> parametersInfo);
    }
}
