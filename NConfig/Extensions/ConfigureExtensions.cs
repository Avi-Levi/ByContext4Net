using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Model;
using NConfig.ValueParsers;
using System.Reflection;
using System.Collections;
using NConfig.Exceptions;

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
            Action<Section> buildSection)
        {
            Section section = new Section();
            buildSection(section);
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
                TryBuildValueParserForType(source, type);
            }

            return BuildParseMethodForType(type, source);
        }

        #region build dinamic value parser
        private static void TryBuildValueParserForType(Configure configure, Type type)
        {
            if (type.IsGenericType && configure.OpenGenericValueParserTypes.ContainsKey(type.GetGenericTypeDefinition()))
            {
                IValueParser parser = BuildParserForGenericType(configure, type);

                configure.ValueParsers.Add(type, parser);
            }
            if (type.IsEnum)
            {
                Type enumParserType = typeof(EnumParser<>).MakeGenericType(type);

                configure.ValueParsers.Add(type, (IValueParser)Activator.CreateInstance(enumParserType));
            }
        }
        private static Func<IEnumerable<string>, object> BuildParseMethodForType(Type type, Configure configure)
        {
            IValueParser parser = GetParserForTypeAndThrowIfNotRegistered(type, configure);

            Func<IEnumerable<string>, object> parseMethod = input =>
            {
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

            return parseMethod;
        }
        private static IValueParser BuildParserForGenericType(Configure configure, Type type)
        {
            ThrowIfParserForOpenGenericTypeNotRegistered(type.GetGenericTypeDefinition(), configure);

            Type openGenericParserType = configure.OpenGenericValueParserTypes[type.GetGenericTypeDefinition()];

            Type[] genericArguments = type.GetGenericArguments();

            Type parserType = openGenericParserType.MakeGenericType(genericArguments);

            var argumentsParserTypes = genericArguments.Select(arg => typeof(IValueParser<>).MakeGenericType(arg)).ToArray();

            ConstructorInfo ci = parserType.GetConstructor(argumentsParserTypes);

            if (ci == null)
            {
                throw new TypeNotSupportedException("an open generic value parser must have a constructor that accepts value " +
                    "parsers in the same order and number as the parser's generic arguments definision.", openGenericParserType);
            }

            IEnumerable<IValueParser> genericArgumentsParsers = genericArguments.Select(arg => GetParserForTypeAndThrowIfNotRegistered(arg, configure));


            IValueParser parser = (IValueParser)ci.Invoke(genericArgumentsParsers.ToArray());
            return parser;
        }
        private static void ThrowIfParserForOpenGenericTypeNotRegistered(Type openGenericType, Configure configure)
        {
            if (!configure.OpenGenericValueParserTypes.ContainsKey(openGenericType))
            {
                throw new TypeNotSupportedException
                    ("A value parser was not registered for open generic type," +
                    "you must first register an open generic value parser using 'OpenGenericValueParserTypes' property on the 'Configure' object."
                    , openGenericType);
            }
        }
        private static IValueParser GetParserForTypeAndThrowIfNotRegistered(Type type, Configure configure)
        {
            IValueParser parser = null;
            if (!configure.ValueParsers.TryGetValue(type, out parser))
            {
                throw new TypeNotSupportedException("A value parser was not registered for type," +
                    "you must first register value parser using 'ValueParsers' property on the 'Configure' object.", type);
            }
            else
            {
                return parser;
            }
        }
        #endregion build dinamic value parser
    }
}
