using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConfig.Impl.Translators
{
    public class KeyValuePairTranslator<TKey, TValue> : BaseValueTranslator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairTranslator(BaseValueTranslator<TKey> keyTranslator, BaseValueTranslator<TValue> valueTranslator)
        {
            this.KeyTranslator = keyTranslator;
            this.ValueTranslator = valueTranslator;
        }

        private BaseValueTranslator<TKey> KeyTranslator { get; set; }
        private BaseValueTranslator<TValue> ValueTranslator { get; set; }

        public override KeyValuePair<TKey, TValue> TranslateFromString(string value)
        {
            string[] splitted = value.Split(':');

            return new KeyValuePair<TKey, TValue>(this.KeyTranslator.TranslateFromString(splitted[0]), this.ValueTranslator.TranslateFromString(splitted[1]));
        }
    }

}
