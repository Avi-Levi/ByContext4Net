using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators.SerializeRawString
{
    public class Int32Translator : IValueTranslator<Int32>
    {
        public int Translate(string value)
        {
            return Int32.Parse(value);
        }
    }
    public class LongTranslator : IValueTranslator<long>
    {
        public long Translate(string value)
        {
            return long.Parse(value);
        }
    }
    public class StringTranslator : IValueTranslator<String>
    {
        public String Translate(string value)
        {
            return (String)value;
        }
    }
    public class BooleanTranslator : IValueTranslator<bool>
    {
        public bool Translate(string value)
        {
            return bool.Parse(value);
        }
    }
    public class DoubleTranslator : IValueTranslator<double>
    {
        public double Translate(string value)
        {
            return double.Parse(value);
        }
    }
    public class CharTranslator : IValueTranslator<Char>
    {
        public Char Translate(string value)
        {
            return Char.Parse(value);
        }
    }
    public class EnumTranslator<TEnum> : IValueTranslator<TEnum> where TEnum : struct
    {
        public TEnum Translate(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
    }
}
