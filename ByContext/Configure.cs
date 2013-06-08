using System;
using System.Collections.Generic;
using System.Linq;
using ByContext.SectionProviders;

namespace ByContext
{
    public class Configure
    {
        public const string DefaultSingleValueFilterPolicyName = "DefaultSingleValueFilterPolicy";
        public const string DefaultCollectionFilterPolicyName = "DefaultCollectionFilterPolicy";

        private Configure()
        {
        }

        public static IByContext With(Action<IByContextSettings> configureAction)
        {
            var settings = new ByContextSettings();

            configureAction(settings);

            IDictionary<string, ISectionProvider> providers = settings.ConfigurationDataProviders.SelectMany(x => x.Get()).ToDictionary(x => x.Key, x => x.Value);

            return new ByContext(settings.RuntimeContext, providers);
        }
    }
}
