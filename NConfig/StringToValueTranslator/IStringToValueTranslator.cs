namespace NConfig.StringToValueTranslator
{
    public interface IStringToValueTranslator
    {
        object Translate(string value);
    }
}
