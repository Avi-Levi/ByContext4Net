namespace NConfig.StringToValueTranslator
{
    public abstract class BaseStringToValueTranslator<T> : IStringToValueTranslator
    {
        public abstract T TranslateFromString(string value);

        public object Translate(string value)
        {
            return this.TranslateFromString(value);
        }
    }
}
