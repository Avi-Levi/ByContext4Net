using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParser
{
    /// <summary>
    /// Parses a given string to the supported type.
    /// </summary>
    public interface IValueParser
    {
        object Parse(string value);
    }
}
