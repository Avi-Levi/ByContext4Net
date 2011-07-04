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
    public static class ConfigureExtensions
    {
        public static Configure SetLogger(this Configure source, ILoggerFacade logger)
        {
            source.Logger = logger;
            return source;
        }
        public static Configure AddConfigurationData(this Configure source,
            Func<IEnumerable<Section>> getSections)
        {
            getSections().Select(sec => sec.ToSectionProvider());
            return source;
        }
        public static Configure AddFromSection(this Configure source,
            Action<Section> buildSection)
        {
            Section section = new Section();
            buildSection(section);
            source.SectionsProviders.Add(section.Name, section.ToSectionProvider());
            return source;
        }
        public static Configure SetModelBinder(this Configure source, IModelBinder binder)
        {
            source.ModelBinder = binder;
            return source;
        }
        public static Configure SetRuntimeContext(this Configure source,
            IRuntimeContextProvider provider)
        {
            source.RuntimeContext = provider.Get();
            return source;
        }
        public static Configure SetRuntimeContext(this Configure source,
            Action<IDictionary<string, string>> setRuntimeContext)
        {
            if (source.RuntimeContext == null)
            {
                source.RuntimeContext = new Dictionary<string, string>();
            }
            setRuntimeContext(source.RuntimeContext);
            return source;
        }

        public static Configure AddValueParser<T>(this Configure source,
            Func<string, T> parseMethod)
        {
            IValueParser parser = new DelegateWrapperValueParser<T>(parseMethod);
            source.ValueParsers.Add(typeof(T), parser);

            return source;
        }

        public static Configure AddCollectionValueParser<T>(this Configure source,
            Func<IEnumerable<string>, T> parseMethod)
        {
            IValueParser parser = new DelegateWrapperCollectionValueParser<T>(parseMethod);
            source.ValueParsers.Add(typeof(T), parser);

            return source;
        }

        public static Func<IEnumerable<string>, object> GetValueParser(this Configure source, Type type)
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
