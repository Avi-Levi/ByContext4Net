using System.Collections.Generic;

namespace NConfig
{
    public interface ICollectionValueParser<T> : IValueParser
    {
        T Parse(IEnumerable<string> values);
    }

    public interface IValueParser<T> : IValueParser
    {
        T Parse(string value);
    }
    
    /// <summary>
    /// A marker interface.
    /// </summary>
    public interface IValueParser
    {}
}
