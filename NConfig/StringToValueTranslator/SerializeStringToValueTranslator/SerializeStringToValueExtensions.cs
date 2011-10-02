using System;

namespace NConfig.StringToValueTranslator.SerializeStringToValueTranslator
{
    public static class SerializeStringToValueExtensions
    {
        public static Configure AddSerializeRawStringTranslator<T>(this Configure source, Func<string, T> translateMethod)
        {
            SerializeStringToValueTranslatorProvider provider = GetProvider(source);

            IStringToValueTranslator translator = new DelegateWrapperTranslator<T>(translateMethod);
            provider.Translators.Add(typeof(T), translator);

            return source;
        }

        private static SerializeStringToValueTranslatorProvider GetProvider(Configure source)
        {
            return source.GetTranslatorProvider<SerializeStringToValueTranslatorProvider>(SerializeStringToValueTranslatorProvider.ProviderKey);
        }
    }
}
