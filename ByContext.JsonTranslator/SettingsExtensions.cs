using ByContext.JsonTranslator;

namespace ByContext
{
    public static class SettingsExtensions
    {
        public static IByContextSettings AddJsonTranslator(this IByContextSettings source)
        {
            return source.AddTranslatorProvider(JsonTranslatorProvider.ProviderKey, new JsonTranslatorProvider());
        }
    }
}