using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NConfig.Exceptions;

namespace NConfig.StringToValueTranslator
{
    public class OpenGenericStringToValueTranslatorProviderDecorator : IStringToValueTranslatorProvider
    {
        public OpenGenericStringToValueTranslatorProviderDecorator(IStringToValueTranslatorProvider inner)
        {
            this.Inner = inner;
            this.TranslatorsCache = new Dictionary<Type, IStringToValueTranslator>();

            this.OpenGenericTranslatorsTypes = new Dictionary<Type, Type>
            { 
                { typeof(KeyValuePair<,>), typeof(KeyValuePairTranslator<,>) },
            };
        }

        public IStringToValueTranslatorProvider Inner { get; set; }
        
        public IDictionary<Type, Type> OpenGenericTranslatorsTypes { get; private set; }

        public IDictionary<Type, IStringToValueTranslator> TranslatorsCache { get; private set; }

        public IStringToValueTranslator Get(Type type)
        {
            if (!this.TranslatorsCache.ContainsKey(type))
            {
                IStringToValueTranslator translator = GetTranslator(type);
                this.TranslatorsCache.Add(type, translator);
            }

            return this.TranslatorsCache[type]; 
        }

        private IStringToValueTranslator GetTranslator(Type type)
        {
            if (type.IsGenericType)
            {
                return BuildTranslatorForOpenGenericType(type);
            }
            else
            {
                return this.Inner.Get(type);
            }
        }

        private IStringToValueTranslator BuildTranslatorForOpenGenericType(Type type)
        {
            if (this.OpenGenericTranslatorsTypes.ContainsKey(type.GetGenericTypeDefinition()))
            {
                return this.BuildTranslatorForGenericType(type);
            }
            else
            {
                throw new TypeNotSupportedException(string.Format(
                    "A translator was not registered for open generic type," +
                    "you must first register an open generic translator using 'OpenGenericValueParserTypes' property on the 'Configure' object."
                    , type.GetGenericTypeDefinition().FullName));
            }
        }

        private IStringToValueTranslator BuildTranslatorForGenericType(Type type)
        {
            Type openGenericTranslatorType = this.OpenGenericTranslatorsTypes[type.GetGenericTypeDefinition()];

            Type[] genericArguments = type.GetGenericArguments();

            Type translatorType = openGenericTranslatorType.MakeGenericType(genericArguments);

            var argumentsTranslatorTypes = genericArguments.Select(arg => typeof(BaseStringToValueTranslator<>).MakeGenericType(arg)).ToArray();

            ConstructorInfo ci = translatorType.GetConstructor(argumentsTranslatorTypes);

            if (ci == null)
            {
                throw new TypeNotSupportedException(string.Format("an open generic translator must have a constructor that accepts value " +
                    "translators in the same order and number as the translator's generic arguments definision.", openGenericTranslatorType.FullName));
            }

            IEnumerable<IStringToValueTranslator> genericArgumentsTranslators = genericArguments.Select(argType => this.Inner.Get(argType));

            IStringToValueTranslator translator = (IStringToValueTranslator)ci.Invoke(genericArgumentsTranslators.ToArray());

            return translator;
        }
    }
}
