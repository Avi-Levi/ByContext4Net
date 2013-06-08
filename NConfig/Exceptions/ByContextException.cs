using System;

namespace ByContext.Exceptions
{
    /// <summary>
    /// A base type for all library's exceptions.
    /// </summary>
    public abstract class ByContextException : Exception
    {
        protected ByContextException(string message, Exception inner)
            : base(message, inner)
        { }
        protected ByContextException(string message)
            : base(message)
        { }
    }
}
