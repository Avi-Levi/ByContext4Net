using System.Collections.Generic;

namespace NConfig.ParameterValueProviders
{
    public interface IParameterValueProvider
    {
        object Get(IDictionary<string, string> runtimeContext);
    }
}
