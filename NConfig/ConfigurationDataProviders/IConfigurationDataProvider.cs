using System.Collections.Generic;
using ByContext.SectionProviders;

namespace ByContext.ConfigurationDataProviders
{
    /// <summary>
    /// Represents a configuration data source.
    /// <remarks>
    /// Provides an extension point that enables configuration to be provided by multiple, different kinds of sources e.g database, file system, by code...
    /// </remarks>
    /// </summary>
    public interface IConfigurationDataProvider
    {
        IDictionary<string, ISectionProvider> Get();
    }
}
