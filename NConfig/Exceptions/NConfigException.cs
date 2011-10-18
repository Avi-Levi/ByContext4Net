using System;

namespace NConfig.Exceptions
{
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
