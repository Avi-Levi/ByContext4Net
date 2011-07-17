using System.Collections.Generic;

namespace NConfig
{
    public interface ICollectionValueTranslator<T> : IValueTranslator
    {
        T Translate(IEnumerable<string> values);
    }

    public interface IValueTranslator<T> : IValueTranslator
    {
        T Translate(string value);
    }
    
    /// <summary>
    /// A marker interface.
    /// </summary>
    public interface IValueTranslator
    {}
}
