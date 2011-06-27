using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.ValueParsers
{
    public class KeyValuePairValueParser<TKey, TValue> : IValueParser<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairValueParser(IValueParser<TKey> keyBinder, IValueParser<TValue> valueBinder)
        {
            this.KeyBinder = keyBinder;
            this.ValueBinder = valueBinder;
        }

        private IValueParser<TKey> KeyBinder { get; set; }
        private IValueParser<TValue> ValueBinder { get; set; }

        public KeyValuePair<TKey, TValue> Parse(string value)
        {
            string[] splitted = value.Split(':');

            return new KeyValuePair<TKey, TValue>(this.KeyBinder.Parse(splitted[0]), this.ValueBinder.Parse(splitted[1]));
        }
    }

}
