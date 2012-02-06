using System;

namespace NConfig.Exceptions
{
    /// <summary>
    /// A base type for all library's exceptions.
    /// </summary>
    public abstract class NConfigException : Exception
    {
        protected NConfigException(string message, Exception inner)
            : base(message, inner)
        { }
        protected NConfigException(string message)
            : base(message)
        { }
    }
}
