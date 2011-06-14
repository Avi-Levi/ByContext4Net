using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.TypeBinders
{
    public class Int32Binder : ITypeBinder<Int32>
    {
        public Int32 Bind(object value)
        {
            string stringValue = (string)value;
            return Int32.Parse(stringValue);
        }
    }
}
