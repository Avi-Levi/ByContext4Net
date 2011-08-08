using System;
using System.Collections.Generic;
using NConfig.Configuration;
using NConfig.Abstractions;
using NConfig.Impl.Translators;
using NConfig.Impl;
using System.Collections;

namespace NConfig
{
    public static class ConfigureExtensions
    {
        public static Configure AddConfigurationDataProvider(this Configure source, IConfigurationDataProvider provider)
        {
            source.ConfigurationDataProviders.Add(provider);
            return source;
        }
        public static Configure AddSection(this Configure source, Section section)
        {
            source.ConfigurationDataProviders.Add(new ConvertFromSectionProvider(() => new Section[1] { section },source));
            return source;
        }
        public static Configure ModelBinder(this Configure source, IModelBinder binder)
        {
            source.ModelBinder = binder;
            return source;
        }
        public static Configure RuntimeContext(this Configure source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static Configure RuntimeContext(this Configure source,Func<IDictionary<string, string>> getContext)
        {
            source.RuntimeContext = getContext();
            return source;
        }
        public static Configure RuntimeContext(this Configure source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            source.RuntimeContext = new Dictionary<string, string>();
            setRuntimeContext(source.RuntimeContext);
            return source;
        }
        public static Configure SingleValueDefaultFilterPolicy(this Configure source, IFilterPolicy policy)
        {
            source.FilterPolicies.Remove(Configure.DefaultSingleValueFilterPolicyName);
            source.FilterPolicies.Add(Configure.DefaultSingleValueFilterPolicyName, policy);

            return source;
        }
        
        #region TranslatorProvider
        public static Configure AddTranslatorProvider(this Configure source, string name, IValueTranslatorProvider provider)
        {
            source.TranslatorProviders.Add(name, new OpenGenericTranslatorProviderDecorator(provider));

            return source;
        }
        public static TProvider GetTranslatorProvider<TProvider>(this Configure source, string name)
            where TProvider : IValueTranslatorProvider
        {
            var provider = source.TranslatorProviders[name];

            var decorator = provider as OpenGenericTranslatorProviderDecorator;
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
