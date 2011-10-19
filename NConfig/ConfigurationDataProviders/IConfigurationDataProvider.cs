using System.Collections.Generic;
using NConfig.SectionProviders;

namespace NConfig.ConfigurationDataProviders
{
    /// <summary>
    /// Defines a source of configuration sections.
    /// <remarks>
    /// Used as an extension point for providing configutation from different sources.
    /// </remarks>
    /// </summary>
    public interface IConfigurationDataProvider
    {
        IDictionary<string, ISectionProvider> Get();
    }
}
