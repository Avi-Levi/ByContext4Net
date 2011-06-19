using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.TypeParsers;
using System.Reflection;
using System.Collections;

namespace NConfig
{
    public static class BuilderExtensions
    {
        public static ConfigurationServiceBuilder SetLogger(this ConfigurationServiceBuilder source, ILoggerFacade logger)
        {
            source.Logger = logger;
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source,
            IConfigurationDataProvider provider)
        {
            source.ConfigurationDataRepository.Sections.AddRange(provider.GetSections());
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source,
            Func<IEnumerable<Section>> getSections)
        {
            source.ConfigurationDataRepository.Sections.AddRange(getSections());
            return source;
        }
        public static ConfigurationServiceBuilder AddConfigurationData(this ConfigurationServiceBuilder source,
            Action<Section> buildSection)
        {
            Section section = new Section();
            buildSection(section);
            source.ConfigurationDataRepository.Sections.Add(section);
            return source;
        }
        public static ConfigurationServiceBuilder SetModelBinder(this ConfigurationServiceBuilder source, IModelBinder binder)
        {
            source.ModelBinder = binder;
            return source;
        }
        public static ConfigurationServiceBuilder SetRuntimeContext(this ConfigurationServiceBuilder source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static ConfigurationServiceBuilder SetRuntimeContext(this ConfigurationServiceBuilder source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            if (source.RuntimeContext == null)
            {
                source.RuntimeContext = new Dictionary<string, string>();
            }
            setRuntimeContext(source.RuntimeContext);
            return source;
        }

        public static ConfigurationServiceBuilder AddTypeParser<T>(this ConfigurationServiceBuilder source,
            Func<string, T> parseMethod)
        {
            ITypeParser parser = new DelegateWrapperTypeParser<T>(parseMethod);
            source.TypeParsers.Add(typeof(T), parser);

            return source;
        }

        public static ConfigurationServiceBuilder AddCollectionTypeParser<T>(this ConfigurationServiceBuilder source,
            Func<IEnumerable<string>, T> parseMethod)
        {
            ITypeParser parser = new DelegateWrapperCollectionTypeParser<T>(parseMethod);
            source.TypeParsers.Add(typeof(T), parser);

            return source;
        }

        public static Func<IEnumerable<string>, object> GetTypeParser(this ConfigurationServiceBuilder source, Type type)
        {
            if (!source.TypeParsers.ContainsKey(type))
            {
                if (type.IsGenericType)
                {
                    Type openGenericParserType = source.OpenGenericTypeParserTypes[type.GetGenericTypeDefinition()];

                    Type[] genericArguments = type.GetGenericArguments();
                    
                    Type parserType = openGenericParserType.MakeGenericType(genericArguments);

                    var argumentsParserTypes = genericArguments.Select(arg=>typeof(ITypeParser<>).MakeGenericType(arg)).ToArray();
                    
                    ConstructorInfo ci = parserType.GetConstructor(argumentsParserTypes);

                    IEnumerable<ITypeParser> genericArgumentsParsers = genericArguments.Select(arg => source.TypeParsers[arg]);

                    ITypeParser parser = (ITypeParser)ci.Invoke(genericArgumentsParsers.ToArray());

                    source.TypeParsers.Add(type, parser);     
                }
            }

            Func<IEnumerable<string>, object> result = input =>
            {
                ITypeParser parser = source.TypeParsers[type];
                MethodInfo parseMethodInfo = parser.GetType().GetMethod("Parse");
                if (typeof(ICollectionTypeParser<>).MakeGenericType(type).IsAssignableFrom(parser.GetType()))
                {
                    return parseMethodInfo.Invoke(parser, new object[1] { input });
                }
                else
                {
                    return parseMethodInfo.Invoke(parser, new object[1] { input.Single() });
                }
            };

            return result;
        }
    }
}
