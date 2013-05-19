using System;
using System.Collections.Generic;
using NConfig.ConfigurationDataProviders;
using NConfig.Filters.Policy;
using NConfig.Model;
using NConfig.ModelBinders;
using NConfig.RuntimeContextProviders;
using NConfig.StringToValueTranslator;

namespace NConfig
{
    public static class NConfigSettingsExtensions
    {
        public static INConfigSettings AddConfigurationDataProvider(this INConfigSettings source, IConfigurationDataProvider provider)
        {
            source.ConfigurationDataProviders.Add(provider);
            return source;
        }
        public static INConfigSettings AddSection(this INConfigSettings source, Section section)
        {
            source.ConfigurationDataProviders.Add(new ConvertFromSectionDataProvider(() => new [] { section },source));
            return source;
        }
        public static INConfigSettings ModelBinderFactory(this INConfigSettings source, IModelBinderFactory binderFactory)
        {
            source.ModelBinderFactory = binderFactory;
            return source;
        }
        public static INConfigSettings RuntimeContext(this INConfigSettings source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static INConfigSettings RuntimeContext(this INConfigSettings source,Func<IDictionary<string, string>> getContext)
        {
            source.RuntimeContext = getContext();
            return source;
        }
        public static INConfigSettings RuntimeContext(this INConfigSettings source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            source.RuntimeContext = new Dictionary<string, string>();
            setRuntimeContext(source.RuntimeContext);
            return source;
        }
        public static INConfigSettings SingleValueDefaultFilterPolicy(this INConfigSettings source, IFilterPolicy policy)
        {
            source.FilterPolicies.Remove(Configure.DefaultSingleValueFilterPolicyName);
            source.FilterPolicies.Add(Configure.DefaultSingleValueFilterPolicyName, policy);

            return source;
        }
        
        #region TranslatorProvider
        public static INConfigSettings AddTranslatorProvider(this INConfigSettings source, string name, IStringToValueTranslatorProvider provider)
        {
            source.TranslatorProviders.Add(name, new OpenGenericStringToValueTranslatorProviderDecorator(provider));

            return source;
        }
        public static TProvider GetTranslatorProvider<TProvider>(this INConfigSettings source, string name)
            where TProvider : IStringToValueTranslatorProvider
        {
            var provider = source.TranslatorProviders[name];

            var decorator = provider as OpenGenericStringToValueTranslatorProviderDecorator;
            if (decorator != null)
            {
                return (TProvider)decorator.Inner;
            }
            else
            {
                return (TProvider)provider;
            }
        }
        #endregion TranslatorProvider
    }
}
