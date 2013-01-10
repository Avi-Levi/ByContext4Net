using System;
using System.Collections.Generic;
using System.Linq;
using NConfig.SectionProviders;

namespace NConfig
{
    public class Configure
    {
        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        private Configure()
        {
        }

        public static IConfigurationService With(Action<INConfigSettings> configureAction)
        {
            var settings = new NConfigSettings();

            configureAction(settings);

            IDictionary<string, ISectionProvider> providers = settings.ConfigurationDataProviders.SelectMany(x => x.Get()).ToDictionary(x => x.Key, x => x.Value);

            return new ConfigurationService(settings.RuntimeContext, providers);
        }
    }
}
