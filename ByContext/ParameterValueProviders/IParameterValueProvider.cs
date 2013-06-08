using System.Collections.Generic;

namespace ByContext.ParameterValueProviders
{
    /// <summary>
    /// Provides a configuration parameter value that is relevant to a certain runtime context.
    /// </summary>
    public interface IParameterValueProvider
    {
        /// <summary>
        /// Provide a configuration parameter value that is relevant to the given <paramref name="runtimeContext"/>.
        /// </summary>
        object Get(IDictionary<string, string> runtimeContext);
    }
}
