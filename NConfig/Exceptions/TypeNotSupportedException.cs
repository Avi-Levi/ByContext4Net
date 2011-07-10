using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Exceptions
{
    public class TypeNotSupportedException : ApplicationException
    {
        public TypeNotSupportedException(string message, Type notSupportedType):
            base(string.Format("{0} type: {1}", message, notSupportedType.FullName))
        {}
    }
}
