using System;

namespace NConfig.StringToValueTranslator.SerializeStringToValueTranslator
{
    public static class SerializeStringToValueExtensions
    {
        public static INConfigSettings AddSerializeRawStringTranslator<T>(this INConfigSettings source, Func<string, T> translateMethod)
        {
            SerializeStringToValueTranslatorProvider provider = GetProvider(source);

            IStringToValueTranslator translator = new DelegateWrapperTranslator<T>(translateMethod);
            provider.Translators.Add(typeof(T), translator);

            return source;
        }

        private static SerializeStringToValueTranslatorProvider GetProvider(INConfigSettings source)
        {
            return source.GetTranslatorProvider<SerializeStringToValueTranslatorProvider>(SerializeStringToValueTranslatorProvider.ProviderKey);
        }
    }
}
