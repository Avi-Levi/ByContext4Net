using System;

namespace NConfig.StringToValueTranslator.SerializeStringToValueTranslator
{
    public class Int32Translator : BaseStringToValueTranslator<Int32>
    {
        public override int TranslateFromString(string value)
        {
            return Int32.Parse(value);
        }
    }
    public class LongTranslator : BaseStringToValueTranslator<long>
    {
        public override long TranslateFromString(string value)
        {
            return long.Parse(value);
        }
    }
    public class StringTranslator : BaseStringToValueTranslator<String>
    {
        public override String TranslateFromString(string value)
        {
            return (String)value;
        }
    }
    public class BooleanTranslator : BaseStringToValueTranslator<bool>
    {
        public override bool TranslateFromString(string value)
        {
            return bool.Parse(value);
        }
    }
    public class DoubleTranslator : BaseStringToValueTranslator<double>
    {
        public override double TranslateFromString(string value)
        {
            return double.Parse(value);
        }
    }
    public class CharTranslator : BaseStringToValueTranslator<Char>
    {
        public override Char TranslateFromString(string value)
        {
            return Char.Parse(value);
        }
    }
    public class TypeTranslator : BaseStringToValueTranslator<Type>
    {
        public override Type TranslateFromString(string value)
        {
            return Type.GetType(value, true);
        }
    }

    public class EnumTranslator<TEnum> : BaseStringToValueTranslator<TEnum> where TEnum : struct
    {
        public override TEnum TranslateFromString(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
    }

    public class UriTranslator : BaseStringToValueTranslator<Uri>
    {
        public override Uri TranslateFromString(string value)
        {
            return new Uri(value);
        }
    }
}
