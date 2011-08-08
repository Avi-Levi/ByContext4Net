using System.Collections.Generic;

namespace NConfig
{
    public abstract class BaseValueTranslator<T> : IValueTranslator
    {
        public abstract T TranslateFromString(string value);

        public object Translate(string value)
        {
            return this.TranslateFromString(value);
        }
    }
    
    /// <summary>
    /// A marker interface.
    /// </summary>
    public interface IValueTranslator
    {
        object Translate(string value);
    }
}
