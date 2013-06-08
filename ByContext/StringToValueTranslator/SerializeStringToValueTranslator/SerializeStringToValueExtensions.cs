using System;

namespace ByContext.StringToValueTranslator.SerializeStringToValueTranslator
{
    public static class SerializeStringToValueExtensions
    {
        public static IByContextSettings AddSerializeRawStringTranslator<T>(this IByContextSettings source, Func<string, T> translateMethod)
        {
            SerializeStringToValueTranslatorProvider provider = GetProvider(source);

            IStringToValueTranslator translator = new DelegateWrapperTranslator<T>(translateMethod);
            provider.Translators.Add(typeof(T), translator);

            return source;
        }

        private static SerializeStringToValueTranslatorProvider GetProvider(IByContextSettings source)
        {
            return source.GetTranslatorProvider<SerializeStringToValueTranslatorProvider>(SerializeStringToValueTranslatorProvider.ProviderKey);
        }
    }
}
