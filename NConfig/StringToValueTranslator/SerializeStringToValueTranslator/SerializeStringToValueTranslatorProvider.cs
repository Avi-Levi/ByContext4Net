using System;
using System.Collections.Generic;
using NConfig.Exceptions;

namespace NConfig.StringToValueTranslator.SerializeStringToValueTranslator
{
    public class SerializeStringToValueTranslatorProvider : IStringToValueTranslatorProvider
    {
        public SerializeStringToValueTranslatorProvider()
        {
            this.Init();
        }

        public const string ProviderKey = "SerializeRawString";
        public IDictionary<Type, IStringToValueTranslator> Translators { get; private set; }

        public IStringToValueTranslator Get(Type type)
        {
            if (!this.Translators.ContainsKey(type))
            {
                if (type.IsEnum)
                {
                    Type enumTranslatorType = typeof(EnumTranslator<>).MakeGenericType(type);
                    IStringToValueTranslator trsnslator = (IStringToValueTranslator)Activator.CreateInstance(enumTranslatorType);
                    return trsnslator;
                }
                else
                {
                    throw new TypeNotSupportedException(string.Format(
                        "A raw value translator was not registered for type {0},you must first register value translator using 'ValueParsers'" +
                        " property on the 'Configure' object.", type.FullName));
                }
            }

            return this.Translators[type];
        }

        private void Init()
        {
            this.Translators = new Dictionary<Type, IStringToValueTranslator>();

            this.Translators.Add(typeof(Int32), new Int32Translator());
            this.Translators.Add(typeof(long), new LongTranslator());
            this.Translators.Add(typeof(string), new StringTranslator());
            this.Translators.Add(typeof(bool), new BooleanTranslator());
            this.Translators.Add(typeof(double), new DoubleTranslator());
            this.Translators.Add(typeof(char), new CharTranslator());
            this.Translators.Add(typeof(Type), new TypeTranslator());
            this.Translators.Add(typeof(Uri), new UriTranslator());
        }
    }
}
