using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators.SerializeRawString
{
    public class Int32Translator : BaseValueTranslator<Int32>
    {
        public override int TranslateFromString(string value)
        {
            return Int32.Parse(value);
        }
    }
    public class LongTranslator : BaseValueTranslator<long>
    {
        public override long TranslateFromString(string value)
        {
            return long.Parse(value);
        }
    }
    public class StringTranslator : BaseValueTranslator<String>
    {
        public override String TranslateFromString(string value)
        {
            return (String)value;
        }
    }
    public class BooleanTranslator : BaseValueTranslator<bool>
    {
        public override bool TranslateFromString(string value)
        {
            return bool.Parse(value);
        }
    }
    public class DoubleTranslator : BaseValueTranslator<double>
    {
        public override double TranslateFromString(string value)
        {
            return double.Parse(value);
        }
    }
    public class CharTranslator : BaseValueTranslator<Char>
    {
        public override Char TranslateFromString(string value)
        {
            return Char.Parse(value);
        }
    }
    public class TypeTranslator : BaseValueTranslator<Type>
    {
        public override Type TranslateFromString(string value)
        {
            return Type.GetType(value, true);
        }
    }

    public class EnumTranslator<TEnum> : BaseValueTranslator<TEnum> where TEnum : struct
    {
        public override TEnum TranslateFromString(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
    }

    public class UriTranslator : BaseValueTranslator<Uri>
    {
        public override Uri TranslateFromString(string value)
        {
            return new Uri(value);
        }
    }
}
