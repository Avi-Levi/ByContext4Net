// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace ByContext.StringToValueTranslator.SerializeStringToValueTranslator
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
