using System.Collections.Generic;
namespace NConfig.Abstractions
{
    public interface IParameterValueProvider
    {
        object Get(IDictionary<string, string> runtimeContext);
    }
}
