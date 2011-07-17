using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NConfig.Abstractions;

namespace NConfig.Impl.Translators.SerializeRawString
{
    public static class SerializeRawStringExtensions
    {
        public static Configure AddSerializeRawStringTranslator<T>(this Configure source, Func<string, T> translateMethod)
        {
            SerializeRawStringTranslatorProvider provider = GetProvider(source);

            IValueTranslator translator = new DelegateWrapperTranslator<T>(translateMethod);
            provider.Translators.Add(typeof(T), translator);

            return source;
        }

        private static SerializeRawStringTranslatorProvider GetProvider(Configure source)
        {
            return source.GetTranslatorProvider<SerializeRawStringTranslatorProvider>(SerializeRawStringTranslatorProvider.ProviderKey);
        }
    }
}
