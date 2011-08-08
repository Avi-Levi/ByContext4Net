using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Configuration;

namespace NConfig
{
    public interface IModelBinder
    {
        object Bind(Type modelType, IDictionary<string, object> parametersInfo);
    }
}
