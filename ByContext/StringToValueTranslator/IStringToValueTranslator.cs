using ByContext.StringToValueTranslator.SerializeStringToValueTranslator;

namespace ByContext.StringToValueTranslator
{
    /// <summary>
    /// Provides an abstraction for resolving configuration values from given strings.
    /// Provides an extension point for resolving string configuration values in different ways.
    /// <remarks>
    /// Implementation that is provided with the library is <see cref="SerializeStringToValueTranslatorProvider"/>
    /// which deserializes the given string.
    /// </remarks>
    /// </summary>
    public interface IStringToValueTranslator
    {
        /// <summary>
        /// Deserializes the given string.
        /// </summary>
        /// <param name="value">Input string to be translated.</param>
        /// <returns>A configuration value.</returns>
        object Translate(string value);
    }
}
