using System;

namespace NConfig.Exceptions
{
    public class TypeNotSupportedException : NConfigException
    {
        public TypeNotSupportedException(string message):base(message)
        {}
    }
}
