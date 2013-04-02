namespace NConfig.JsonTranslator
{
    public static class NConfigSettingsExtensions
    {
        public static INConfigSettings AddJsonTranslator(this INConfigSettings source)
        {
            return source.AddTranslatorProvider(JsonTranslatorProvider.ProviderKey, new JsonTranslatorProvider());
        }
    }
}
