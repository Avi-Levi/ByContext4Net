using System;
using System.Collections.Generic;
using NConfig.Model;
using NConfig.Abstractions;
using NConfig.Impl.Translators;

namespace NConfig
{
    public static class ConfigureExtensions
    {
        public static Configure Logger(this Configure source, ILoggerFacade logger)
        {
            source.Logger = logger;
            return source;
        }
        public static Configure AddSection(this Configure source,
            Section section)
        {
            source.SectionsProviders.Add(section.Name, section.ToSectionProvider(source));
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
        public static Configure RuntimeContext(this Configure source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            source.RuntimeContext = new Dictionary<string, string>();
            setRuntimeContext(source.RuntimeContext);
            return source;
        }
        public static Configure TranslatorProvider(this Configure source,string name, IValueTranslatorProvider provider)
        {
            var decoratorProvider = new OpenGenericDecoratorProvider(provider);
            source.TranslatorProviders.Add(name, decoratorProvider);

            return source;
        }
        public static TProvider GetTranslatorProvider<TProvider>(this Configure source, string name) 
            where TProvider : IValueTranslatorProvider
        {
            var provider = source.TranslatorProviders[name];

            var decorator = provider as OpenGenericDecoratorProvider;
            if (decorator != null)
            {
                return (TProvider)decorator.Inner;
            }
            else
            {
                return (TProvider)provider;
            }
        }
    }
}
