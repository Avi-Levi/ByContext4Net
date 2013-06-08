using System.Collections.Generic;

namespace ByContext.RuntimeContextProviders
{
    /// <summary>
    /// Provides the application's runtime context.
    /// </summary>
    public interface IRuntimeContextProvider
    {
        /// <summary>
        /// Provides the application's runtime context.
        /// </summary>
        IDictionary<string, string> Get();
    }
}
