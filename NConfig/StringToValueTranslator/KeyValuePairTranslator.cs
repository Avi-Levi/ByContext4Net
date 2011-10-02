using System.Collections.Generic;

namespace NConfig.StringToValueTranslator
{
    public class KeyValuePairTranslator<TKey, TValue> : BaseStringToValueTranslator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePairTranslator(BaseStringToValueTranslator<TKey> keyTranslator, BaseStringToValueTranslator<TValue> valueTranslator)
        {
            this.KeyTranslator = keyTranslator;
            this.ValueTranslator = valueTranslator;
        }

        private BaseStringToValueTranslator<TKey> KeyTranslator { get; set; }
        private BaseStringToValueTranslator<TValue> ValueTranslator { get; set; }

        public override KeyValuePair<TKey, TValue> TranslateFromString(string value)
        {
            string[] splitted = value.Split(':');

            return new KeyValuePair<TKey, TValue>(this.KeyTranslator.TranslateFromString(splitted[0]), this.ValueTranslator.TranslateFromString(splitted[1]));
        }
    }

}
