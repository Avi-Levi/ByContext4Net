using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.ValueParsers;
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

        public static ConfigurationServiceBuilder AddValueParser<T>(this ConfigurationServiceBuilder source,
            Func<string, T> parseMethod)
        {
            IValueParser parser = new DelegateWrapperValueParser<T>(parseMethod);
            source.ValueParsers.Add(typeof(T), parser);

            return source;
        }

        public static ConfigurationServiceBuilder AddCollectionValueParser<T>(this ConfigurationServiceBuilder source,
            Func<IEnumerable<string>, T> parseMethod)
        {
            IValueParser parser = new DelegateWrapperCollectionValueParser<T>(parseMethod);
            source.ValueParsers.Add(typeof(T), parser);

            return source;
        }

        public static Func<IEnumerable<string>, object> GetValueParser(this ConfigurationServiceBuilder source, Type type)
        {
            if (!source.ValueParsers.ContainsKey(type))
            {
                if (type.IsGenericType && source.OpenGenericValueParserTypes.ContainsKey(type.GetGenericTypeDefinition()))
                {
                    Type openGenericParserType = source.OpenGenericValueParserTypes[type.GetGenericTypeDefinition()];

                    Type[] genericArguments = type.GetGenericArguments();

                    Type parserType = openGenericParserType.MakeGenericType(genericArguments);

                    var argumentsParserTypes = genericArguments.Select(arg => typeof(IValueParser<>).MakeGenericType(arg)).ToArray();

                    ConstructorInfo ci = parserType.GetConstructor(argumentsParserTypes);

                    IEnumerable<IValueParser> genericArgumentsParsers = genericArguments.Select(arg => source.ValueParsers[arg]);

                    IValueParser parser = (IValueParser)ci.Invoke(genericArgumentsParsers.ToArray());

                    source.ValueParsers.Add(type, parser);
                }
            }

            Func<IEnumerable<string>, object> result = input =>
            {
                IValueParser parser = source.ValueParsers[type];
                MethodInfo parseMethodInfo = parser.GetType().GetMethod("Parse");
                if (typeof(ICollectionValueParser<>).MakeGenericType(type).IsAssignableFrom(parser.GetType()))
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
