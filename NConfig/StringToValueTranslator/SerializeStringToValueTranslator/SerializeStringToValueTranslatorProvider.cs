using System;
using System.Collections.Generic;
using NConfig.Exceptions;

namespace NConfig.StringToValueTranslator.SerializeStringToValueTranslator
{
    public class SerializeStringToValueTranslatorProvider : IStringToValueTranslatorProvider
    {
        public SerializeStringToValueTranslatorProvider()
        {
            this.Translators = new Dictionary<Type, IStringToValueTranslator>
                                   {
                                       {typeof (Int32), new Int32Translator()},
                                       {typeof (long), new LongTranslator()},
                                       {typeof (string), new StringTranslator()},
                                       {typeof (bool), new BooleanTranslator()},
                                       {typeof (double), new DoubleTranslator()},
                                       {typeof (char), new CharTranslator()},
                                       {typeof (Type), new TypeTranslator()},
                                       {typeof (Uri), new UriTranslator()}
                                   };
        }

        public const string ProviderKey = "SerializeRawString";
        public IDictionary<Type, IStringToValueTranslator> Translators { get; private set; }

        public IStringToValueTranslator Get(Type type)
        {
            if (!this.Translators.ContainsKey(type))
            {
                if (type.IsEnum)
                {
                    var enumTranslatorType = typeof(EnumTranslator<>).MakeGenericType(type);
                    var trsnslator = (IStringToValueTranslator)Activator.CreateInstance(enumTranslatorType);
                    return trsnslator;
                }
                else
                {
                    throw new TypeTranslatorNotRegisteredException("Serialize String To Value", type);
                }
            }

            return this.Translators[type];
        }
    }
}
