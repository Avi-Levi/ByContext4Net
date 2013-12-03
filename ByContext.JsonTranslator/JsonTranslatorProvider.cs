using System;
using ByContext.StringToValueTranslator;

namespace ByContext.JsonTranslator
{
    public class JsonTranslatorProvider: IStringToValueTranslatorProvider
    {
        public const string ProviderKey = "Json";
 
        public IStringToValueTranslator Get(Type type)
        {
            return new JsonTranslator(type);
        }
    }
}