using System.Collections.Generic;

namespace NConfig
{
    public interface ICollectionTypeParser<T> : ITypeParser
    {
        T Parse(IEnumerable<string> value);
    }

    public interface ITypeParser<T> : ITypeParser
    {
        T Parse(string value);
    }
    
    /// <summary>
    /// A marker interface.
    /// </summary>
    public interface ITypeParser
    {}
}
