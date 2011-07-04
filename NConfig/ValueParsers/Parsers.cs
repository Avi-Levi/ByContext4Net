using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParsers
{
    public class Int32Parser : IValueParser<Int32>
    {
        public int Parse(string value)
        {
            return Int32.Parse(value);
        }
    }
    public class LongParser : IValueParser<long>
    {
        public long Parse(string value)
        {
            return long.Parse(value);
        }
    }
    public class StringParser : IValueParser<String>
    {
        public String Parse(string value)
        {
            return (String)value;
        }
    }
    public class BooleanParser : IValueParser<bool>
    {
        public bool Parse(string value)
        {
            return bool.Parse(value);
        }
    }
    public class DoubleParser : IValueParser<double>
    {
        public double Parse(string value)
        {
            return double.Parse(value);
        }
    }
    public class CharParser : IValueParser<Char>
    {
        public Char Parse(string value)
        {
            return Char.Parse(value);
        }
    }
}
