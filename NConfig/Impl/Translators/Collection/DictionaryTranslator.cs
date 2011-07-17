using System.Collections.Generic;
using System.Linq;

namespace NConfig.Impl.Translators.Collection
{
    public class DictionaryTranslator<TKey, TValue> : 
        CollectionTranslatorBase<IDictionary<TKey, TValue>,
        KeyValuePair<TKey, TValue>>
    {
        public DictionaryTranslator(IValueTranslator<TKey> keyTranslator, IValueTranslator<TValue> valueTranslator) :
            base(new KeyValuePairTranslator<TKey, TValue>(keyTranslator, valueTranslator))
        {}

        protected override IDictionary<TKey, TValue> ConvertToResultCollection(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
