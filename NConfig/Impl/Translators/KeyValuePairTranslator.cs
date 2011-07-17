using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators
{
    public class KeyValuePairTranslator<TKey, TValue> : IValueTranslator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairTranslator(IValueTranslator<TKey> keyBinder, IValueTranslator<TValue> valueBinder)
        {
            this.KeyBinder = keyBinder;
            this.ValueBinder = valueBinder;
        }

        private IValueTranslator<TKey> KeyBinder { get; set; }
        private IValueTranslator<TValue> ValueBinder { get; set; }

        public KeyValuePair<TKey, TValue> Translate(string value)
        {
            string[] splitted = value.Split(':');

            return new KeyValuePair<TKey, TValue>(this.KeyBinder.Translate(splitted[0]), this.ValueBinder.Translate(splitted[1]));
        }
    }

}
