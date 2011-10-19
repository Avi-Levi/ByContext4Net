using System.Collections.Generic;

namespace NConfig.SectionProviders
{
    /// <summary>
    /// Provides an initialized instance of a configuration section.
    /// </summary>
    public interface ISectionProvider
    {
        /// <summary>
        /// Provides an instance of a configuration section initialized according to the given <paramref name="runtimeContext"/>.
        /// </summary>
        /// <param name="runtimeContext">the application's runtime context.</param>
        /// <returns>Initialized instance of a configuration section</returns>
        object Get(IDictionary<string, string> runtimeContext);
    }
}
