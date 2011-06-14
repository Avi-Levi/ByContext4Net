using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;

namespace NConfig
{
    public interface IModelBinder
    {
        object Bind(Type modelType, IDictionary<string, object> parametersInfo);
    }
}
