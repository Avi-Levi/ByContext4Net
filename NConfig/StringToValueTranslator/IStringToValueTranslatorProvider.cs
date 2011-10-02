using System;

namespace NConfig.StringToValueTranslator
{
    public interface IStringToValueTranslatorProvider
    {
        IStringToValueTranslator Get(Type type);
    }
}
