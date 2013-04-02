using System;
using NConfig.StringToValueTranslator;

namespace NConfig.JsonTranslators
{
    class JsonTranslatorProvider : IStringToValueTranslatorProvider
    {
        public const string ProviderKey = "Json";

        public IStringToValueTranslator Get(Type type)
        {
            return new JsonTranslator(type);
        }
    }
}
