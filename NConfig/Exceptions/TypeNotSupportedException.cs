using System;

namespace NConfig.Exceptions
{
    public class TypeNotSupportedException : ApplicationException
    {
        public TypeNotSupportedException(string message):base(message)
        {}
    }
}
