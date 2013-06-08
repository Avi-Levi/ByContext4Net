using System;

namespace ByContext.StringToValueTranslator
{
    /// <summary>
    /// provides instances of <see cref="IStringToValueTranslator"/> for a given <see cref="System.Type"/>
    /// </summary>
    public interface IStringToValueTranslatorProvider
    {
        /// <summary>
        /// Returns an instance of <see cref="IStringToValueTranslator"/> forthe given <paramref name="type"/>.
        /// </summary>
        IStringToValueTranslator Get(Type type);
    }
}
