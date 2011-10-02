using System.Collections.Generic;
using NConfig.SectionProviders;

namespace NConfig.ConfigurationDataProviders
{
    public interface IConfigurationDataProvider
    {
        IDictionary<string, ISectionProvider> Get();
    }
}
